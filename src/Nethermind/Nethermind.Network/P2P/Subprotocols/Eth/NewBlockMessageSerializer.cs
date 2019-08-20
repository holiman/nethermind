﻿/*
 * Copyright (c) 2018 Demerzel Solutions Limited
 * This file is part of the Nethermind library.
 *
 * The Nethermind library is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * The Nethermind library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with the Nethermind. If not, see <http://www.gnu.org/licenses/>.
 */

using System.Numerics;
using DotNetty.Buffers;
using Nethermind.Core;
using Nethermind.Core.Encoding;
using Nethermind.Core.Extensions;
using Nethermind.Dirichlet.Numerics;

namespace Nethermind.Network.P2P.Subprotocols.Eth
{
    public class NewBlockMessageSerializer : IMessageSerializer<NewBlockMessage>
    {
        private BlockDecoder _blockDecoder = new BlockDecoder();

        public byte[] Serialize(NewBlockMessage message)
        {
            int contentLength = _blockDecoder.GetLength(message.Block, RlpBehaviors.None) + Rlp.LengthOf((UInt256) message.TotalDifficulty);
            int totalLength = Rlp.LengthOfSequence(contentLength);
            RlpStream rlpStream = new RlpStream(totalLength);
            rlpStream.StartSequence(contentLength);
            rlpStream.Encode(message.Block);
            rlpStream.Encode((UInt256) message.TotalDifficulty);
            return rlpStream.Data;
        }

        public NewBlockMessage Deserialize(byte[] bytes)
        {
            RlpStream rlpStream = bytes.AsRlpStream();
            NewBlockMessage message = new NewBlockMessage();
            rlpStream.ReadSequenceLength();
            message.Block = Rlp.Decode<Block>(rlpStream);
            message.TotalDifficulty = rlpStream.DecodeUInt256();
            return message;
        }
    }

    public class ZeroNewBlockMessageSerializer : IZeroMessageSerializer<NewBlockMessage>
    {
        private BlockDecoder _blockDecoder = new BlockDecoder();

        public void Serialize(IByteBuffer byteBuffer, NewBlockMessage message)
        {
            byteBuffer.WriteByte(message.PacketType);
            int contentLength = _blockDecoder.GetLength(message.Block, RlpBehaviors.None) + Rlp.LengthOf(message.TotalDifficulty);
            RlpStream rlpStream = new NettyRlpStream(byteBuffer);
            rlpStream.StartSequence(contentLength);
            rlpStream.Encode(message.Block);
            rlpStream.Encode(message.TotalDifficulty);
        }

        public void Serialize2(IByteBuffer byteBuffer, NewBlockMessage message)
        {
            byteBuffer.WriteByte(message.PacketType);
            int contentLength = _blockDecoder.GetLength(message.Block, RlpBehaviors.None) + Rlp.LengthOf(message.TotalDifficulty);
            RlpStream rlpStream = new NettyRlpStream(byteBuffer);
        }
        
        public void Serialize3(IByteBuffer byteBuffer, NewBlockMessage message)
        {
            byteBuffer.WriteByte(message.PacketType);
            int contentLength = _blockDecoder.GetLength(message.Block, RlpBehaviors.None) + Rlp.LengthOf(message.TotalDifficulty);
            RlpStream rlpStream = new NettyRlpStream(byteBuffer);
            rlpStream.StartSequence(contentLength);
        }
        
        public void Serialize4(IByteBuffer byteBuffer, NewBlockMessage message)
        {
            byteBuffer.WriteByte(message.PacketType);
            int contentLength = _blockDecoder.GetLength(message.Block, RlpBehaviors.None) + Rlp.LengthOf(message.TotalDifficulty);
            RlpStream rlpStream = new NettyRlpStream(byteBuffer);
            rlpStream.StartSequence(contentLength);
            rlpStream.Encode(message.Block);
        }

        public NewBlockMessage Deserialize(IByteBuffer byteBuffer)
        {
            RlpStream rlpStream = new NettyRlpStream(byteBuffer);
            NewBlockMessage message = new NewBlockMessage();
            rlpStream.ReadSequenceLength();
            message.Block = Rlp.Decode<Block>(rlpStream);
            message.TotalDifficulty = rlpStream.DecodeUInt256();
            return message;
        }
    }
}