using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Communication
{
    /// <summary>
    /// 缓存通讯数据
    /// </summary>
    public class ByteBuffer
    {
        private object _lock = new object();

        private byte[] _buffer;

        public byte[] Buffer
        {
            get { return _buffer; }
            set { _buffer = value; }
        }

        private long writeIndex = 0;

        private long readIndex = 0;

        private long capacity;

        /// <summary>
        /// 还未读的字节数
        /// </summary>
        private long unreadCount = 0;

        public ByteBuffer(long capacity)
        {
            this.capacity = capacity;
            _buffer = new byte[capacity];
        }

        /// <summary>
        /// 向缓冲中添加数据
        /// </summary>
        /// <param name="b"></param>
        public void Add(byte b)
        {
            lock (_lock)
            {
                _buffer[writeIndex] = b;
                writeIndex++;
                if (unreadCount == capacity)
                {
                    readIndex++;
                    if (readIndex >= capacity)
                    {
                        readIndex = 0;
                    }
                }
                else
                {
                    unreadCount++;
                }

                if (writeIndex >= capacity)
                {
                    writeIndex = 0;
                }
            }
        }

        /// <summary>
        /// 向缓冲中添加数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public void Add(byte[] buffer, int offset, int length)
        {
            for (int i = offset; i < offset + length; i++)
            {
                Add(buffer[i]);
            }
        }

        /// <summary>
        /// 从缓冲中取一个字节
        /// </summary>
        /// <returns></returns>
        private byte? GetByte()
        {
            if (unreadCount == 0) return null;

            byte result = _buffer[readIndex];
            readIndex++;
            unreadCount--;

            if (readIndex >= capacity)
            {
                readIndex = 0;
            }
            return result;
        }

        /// <summary>
        /// 搜索并定位
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private bool SearchByte(byte b)
        {
            for (int i = 0; i < capacity; i++)
            {
                if (i >= unreadCount) return false;

                if (readIndex + i < capacity)
                {
                    if (_buffer[readIndex + i] == b)
                    {
                        unreadCount = unreadCount - i;
                        readIndex = readIndex + i;
                        return true;
                    }
                }
                else
                {
                    if (_buffer[readIndex + i - capacity] == b)
                    {
                        unreadCount = unreadCount - i;
                        readIndex = readIndex + i - capacity;
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 获取缓存中的数据
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] GetBytes(int length)
        {
            lock (_lock)
            {
                if (length > unreadCount) return null;

                byte[] result = new byte[length];
                for (int i = 0; i < length; i++)
                {
                    result[i] = GetByte().Value;
                }
                return result;
            }
        }

        /// <summary>
        /// 获取缓存中的数据
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] GetBytes(byte prefix, int length)
        {
            lock (_lock)
            {
                if (!SearchByte(prefix))
                {
                    return null;
                }

                if (length > unreadCount) return null;

                byte[] result = new byte[length];
                for (int i = 0; i < length; i++)
                {
                    result[i] = GetByte().Value;
                }
                return result;
            }
        }

        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes()
        {
            lock (_lock)
            {
                long length = 0;
                if (writeIndex >= readIndex)
                {
                    length = writeIndex - readIndex;
                }
                else
                {
                    length = capacity - readIndex + writeIndex;
                }

                byte[] result = new byte[length];

                for (int i = 0; i < length; i++)
                {
                    result[i] = GetByte().Value;
                }
                return result.Length == 0 ? null : result;
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                readIndex = 0;
                writeIndex = 0;
                unreadCount = 0;
            }
        }
    }
}
