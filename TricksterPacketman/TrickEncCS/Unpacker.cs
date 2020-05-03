using System;
using System.Collections.Generic;
using System.Text;

namespace TrickEncCS
{
    class Unpacker
    {
        /// <summary>
        /// This method is copied directly from the Trickster server.
        /// 
        /// My theory on what this method does is that it makes sure the first packet sent is valid.
        /// If it's not valid, we can exit early as the server will know that it's an invalid client.
        /// Perhaps someone's trying to send some trash data or connect with a non-client.
        /// 
        /// The Header object (at the time of a call to this function) is essentially a bunch 
        /// of fields within the raw packet. 
        /// </summary>
        /// <param name="header">The Header object before decrypting the packet header.</param>
        /// <param name="sessionInfo">The current client session.</param>
        /// <returns>Primitive packet validity flag</returns>
        static bool CheckHeader(Header header, SessionInfo sessionInfo)
        {
            if (!sessionInfo.IsStrict || !sessionInfo.IsFirst)
            {
                return true;
            }

            byte packing = header.Packing;
            if (packing > 0xFu && (KeyTable.Table[(ushort)((header.RandKey << 8) + packing)] & 0xF) == 15)
            {
                // If that magic value is 15, we know this is a primitively "valid packet".
                // We can update the session since we know the next packet is no longer the first packet.
                sessionInfo.IsFirst = false;
                return true;
            }

            return false;
        }

        private static Header PacketToHeader(byte[] data)
        {
            return new Header()
            {
                Len = BitConverter.ToUInt16(data, 0),
                Cmd = BitConverter.ToUInt16(data, 2),
                Seq = BitConverter.ToUInt16(data, 4),
                RandKey = data[6],
                Packing = data[7],
                CheckFlag = data[8]
            };
        }

        /// <summary>
        /// Updates the length in the header to not have the dummy data.
        /// This is helpful for later, as we don't want to decrypt the dummy data.
        /// Otherwise, that would likely(?) affect the tail checksum value calculation.
        /// </summary>
        /// <param name="packet">The packet data to unpack.</param>
        /// <returns>Whether exclusion was successful or not, also indicating the packet's validity</returns>
        private static bool ExcludeDummy(byte[] packet)
        {
            byte packing = packet[7];

            // The "second round" value of Packing will let us know if there's any dummy data.
            if ((packing & 8) > 0)
            {
                ushort lenNoDummy = (ushort)(packet[0] - (packet[6] % 13));

                if (lenNoDummy < 11) // The length should always be >11 if there's dummy data.
                {
                    return false;
                }

                // Update the packet's length to remove the dummy data.
                Util.CopyTo(packet, 0, lenNoDummy);

                // Time to update the packing value to be its "third round" value.
                packet[7] = (byte)(packing ^ 8);
            }

            return true;
        }

        /// <summary>
        /// Header decryption logic
        /// </summary>
        /// <param name="sessionInfo">The current client session.</param>
        /// <param name="packet">The packet data to unpack.</param>
        /// <returns>A header if successfully decoded, or null if unsuccessful</returns>
        private static Header UnpackHeader(byte[] packet, SessionInfo sessionInfo)
        {
            var checkHeader = CheckHeader(PacketToHeader(packet), sessionInfo);

            if (!checkHeader)
                throw new Exception("Header was invalid");

            var key = sessionInfo.Key;

            byte packing = packet[7];
            if (packing > 0xFu)
            {
                byte packetStatus = KeyTable.Table[(ushort)((packet[6] << 8) + packing)];
                if (packetStatus > 0xFu || (packetStatus & 2) == 0)
                {
                    // Well, something's off.
                    return null;
                }

                // We can start calculating the packet sequence, opcode, and length now that we know we have
                // at least what we _should_ theoretically need to calculate them.

                ushort sequence = (ushort)((ushort)(KeyTable.Table[(ushort)(((packet[3] ^ packing) << 8) + packet[5])] << 8)
                    + KeyTable.Table[(ushort)(((packet[2] ^ key) << 8) + packet[4])]);

                /// SC - S
                int ecx, eax;

                ecx = packet[7];
                eax = packet[3];

                eax ^= packet[7];
                ecx = packet[5];
                eax <<= 8;
                eax += ecx;
                byte a = KeyTable.Table[(ushort)eax];
                ushort sequenceC = (ushort)(a << 8);

                eax = packet[2];
                ecx = packet[4];
                eax ^= key;
                eax <<= 8;
                eax += ecx;
                byte b = KeyTable.Table[eax];
                sequenceC += b;

                if (sequenceC != sequence)
                {
                    throw new Exception("Seq");
                }

                /// SC - S

                /// OC - S
                ushort opcode = (ushort)((KeyTable.Table[(ushort)(((packet[1] ^ key) << 8) + packet[3])] << 8)
                    + KeyTable.Table[(ushort)(((packet[6] ^ packet[0]) << 8) + packet[2])]);

                // left
                ecx = packet[3];
                eax = packet[1];
                eax ^= key;
                eax <<= 8;
                eax += ecx;
                ushort opcodeC = KeyTable.Table[(ushort)eax];
                opcodeC <<= 8;

                // right
                ecx = packet[0];
                eax = packet[6];
                eax ^= ecx;
                ecx = packet[2];
                eax <<= 8;
                eax += ecx;
                opcodeC += KeyTable.Table[(ushort)eax];

                if (opcodeC != opcode)
                {
                    throw new Exception("Op");
                }

                /// OC - S

                // The length at this point includes the dummy bytes. We'll get rid of that later.
                var length = (ushort)((KeyTable.Table[(ushort)(((packet[6] ^ packet[7]) << 8) + packet[1])] << 8)
                    + KeyTable.Table[(ushort)(((packet[7] ^ key) << 8) + packet[0])]);

                // LE - S

                ecx = packet[7]; // ok
                eax = packet[6];
                eax ^= ecx;
                eax <<= 8;
                eax += packet[1];
                ushort lengthC = (ushort)((KeyTable.Table[(ushort)eax]) << 8);

                eax = packet[7];
                ecx = packet[0];
                eax ^= key;
                eax <<= 8;
                eax += ecx;
                lengthC += KeyTable.Table[(ushort)eax];

                if (lengthC != length)
                {
                    throw new Exception("Le");
                }

                // LE - S

                // Update Packing within the actual packet for later use.
                // This should now be the "second round" value.
                packet[7] = (byte)(packetStatus ^ 2);

                // Now we can overwrite the length, opcode, and sequence of the actual packet.
                // Obviously, we'll use this next.
                Util.CopyTo(packet, 0, lengthC);
                Util.CopyTo(packet, 2, opcodeC);
                Util.CopyTo(packet, 4, sequenceC);
            }

            // Let's check some basic lengths and then verify that the checksum was correct.
            if (BitConverter.ToUInt16(packet, 0) >= 0xBu
                && BitConverter.ToInt32(packet, 0) < 0x7FFFFFFFu
                && Common.MakeChecksum(packet, key) == packet[8])
            {
                // Since all of these checks have passed, we can create a Header from this data.
                return PacketToHeader(packet);
            }

            // A check failed, so we're going to tell the GetHeader method that it failed.
            // It should throw an exception from there.
            return null;
        }

        /// <summary>
        /// Actual data decryption logic and tail checksum validation logic
        /// </summary>
        /// <param name="sessionInfo">The current client session.</param>
        /// <param name="packet">The packet data to unpack.</param>
        /// <returns>Whether we were able to successfully unpack the data</returns>
        private static bool UnpackData(SessionInfo sessionInfo, byte[] packet)
        {
            var header = PacketToHeader(packet); //
            byte magicKey = (byte)(header.Cmd * header.RandKey % 256);

            var pRcvData = Util.ByteCopy(packet);
            int i = 0;
            ushort calcTailValue = 0;

            // Note: The minimum [encrypted] packet length is 11, but the minimum [packet length] should be 9.
            // It's unclear why this is done.
            header.Len -= 2;

            int offset = 0; // This offset will eventually tell us where the tail checksum value is.

            ushort calculatedTail = 0;

            if ((header.Packing & 4) > 0)
            {
                byte v4 = (byte)(header.Cmd * header.RandKey % 256);

                // Possible use: If the packet isn't empty, decrypt the data?
                if (header.Len != 9)
                {
                    do
                    {
                        byte val = KeyTable.Table[(ushort)(((magicKey ^ offset) << 8) + pRcvData[offset + 9])];
                        packet[offset + 9] = val;
                        calculatedTail += val;
                        offset++;
                    }
                    while (offset < header.Len - 9);


                    do
                    {
                        ushort val = pRcvData[i + 9];
                        byte v7 = KeyTable.Table[(ushort)(val + ((v4 ^ i) << 8))];
                        pRcvData[i + 9] = v7;
                        calcTailValue += v7;
                        i++;
                    }
                    while (i < header.Len - 9);

                    Util.AssertByteEqual(pRcvData, packet);
                }

                // This seems to be for the tail checksum.
                // We shouldn't add the tail checksum to the data checksum, of course.
                for (int ie = 0; ie < 2; ie++)
                {
                    packet[offset + 9] = KeyTable.Table[(ushort)(((magicKey ^ offset) << 8) + packet[offset + 9])];
                    offset++;
                }

                var j = 2;
                do
                {
                    ushort val = pRcvData[i + 9];
                    byte v1 = KeyTable.Table[(ushort)(val + ((v4 ^ i) << 8))];
                    pRcvData[i + 9] = v1;
                    i++;
                    j--;
                }
                while (j > 0);

                Util.AssertByteEqual(pRcvData, packet);

                // In a way, tell the later code (the code below that updates the encryption key)
                // what it should actually do.
                header.Packing ^= 4;
            }
            else
            {
                // The Packing value seems to have been non-optimal.
                // This is probably a fallback so that we don't crash immediately.
                // Regardless, this was in the original packet decryption code. I don't know what it does.
                offset = header.Len - 7;
                calculatedTail = (ushort)(header.Len - 9);
            }

            // We've been calculating a "tail checksum" value.
            // Of course, this should match the packet's given tail checksum value.
            if (calculatedTail != BitConverter.ToUInt16(packet, offset + 7))
            {
                return false;
            }

            // Packing can be 0x06 or 0x07.
            // We should update the key if the value is 0x07.
            // If it's 0x06, that means the key stays the same.
            if ((header.Packing ^ 1) > 0)
            {
                // We should store the current key.
                // We'll likely need it if we come across a split whole packet.
                sessionInfo.LastKey = sessionInfo.Key;

                // These shouldn't actually matter anymore.
                // We're already done unpacking, so why even bother?
                /*
                packet[7] = 0;
                header.Packing = (byte)(header.Packing ^ 1);
                */

                // The key update algorithm uses the tail checksum value to "seed" a new key.
                Common.UpdateKey(sessionInfo, calculatedTail);
            }

            return true;
        }

        /// <summary>
        /// Unpacks an encrypted packet from Trickster.
        /// </summary>
        /// <param name="sessionInfo">The current client session.</param>
        /// <param name="packet">The packet data to unpack.</param>
        /// <returns>The length of the data INCLUDING the dummy data.</returns>
        public static ushort Unpack(SessionInfo sessionInfo, byte[] packet)
        {
            var header = UnpackHeader(packet, sessionInfo);
            if (header == null)
                throw new Exception("Header could not be unpacked");

            // Store the full length before excluding the dummy
            // We'll need this so we know how many bytes to skip in a merged packet.
            var fullLen = header.Len;

            bool excludeDummy = ExcludeDummy(packet);
            if (!excludeDummy)
                throw new Exception("Dummy data could not be excluded");

            bool unpackData = UnpackData(sessionInfo, packet);
            if (!unpackData)
                throw new Exception("Data could not be unpacked");

            return fullLen;
        }
    }
}
