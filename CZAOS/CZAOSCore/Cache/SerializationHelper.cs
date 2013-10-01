using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace CZAOSCore
{
    public static class SerializationHelper
    {

        /// <summary>
        /// Deserialize a compressed serialized object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serialized"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string serialized, bool compress) where T : class
        {
            byte[] serializedData = Convert.FromBase64String(serialized);

            return Deserialize<T>(serializedData, compress);
        }

        public static T Deserialize<T>(byte[] serialized, bool compress)
        {
            if (compress)
                serialized = GzipCompression.Decompress(serialized);

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(serialized);
            T data = (T)bf.Deserialize(ms);

            return data;
        }

        public static object DeserializeObject(byte[] serialized, bool compress)
        {
            if (compress)
                serialized = GzipCompression.Decompress(serialized);

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(serialized);
            object data = bf.Deserialize(ms);

            return data;
        }

        /// <summary>
        /// Serialize the given object and optionally compress it using LZMA deflation
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Serialize(object data, bool compress)
        {
            byte[] bytes = SerializeToBinary(data, compress);

            // Serialize to base 64 so it can be stored anywhere
            return Convert.ToBase64String(bytes);

        }

        public static byte[] SerializeToBinary(object data, bool compress)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            MemoryStream objStream = new MemoryStream();
            bf.Serialize(ms, data);

            byte[] bytes = ms.ToArray();

            if (compress)
                bytes = GzipCompression.Compress(bytes);

            return bytes;

        }


        /// <summary>
        /// Deserialize an array of objects from a list of XmlNodes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static T[] Deserialize<T>(XmlNodeList nodes) where T : class
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            List<T> classes = new List<T>(nodes.Count);
            foreach (XmlNode node in nodes)
            {
                T instance = ConvertNode<T>(node, ser);
                classes.Add(instance);
            }

            return classes.ToArray();
        }

        /// <summary>
        /// Deserialize a class that has been decorated with XmlAttributes from an XML Node
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static T Deserialize<T>(XmlNode node) where T : class
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            return ConvertNode<T>(node, ser);
        }

        /// <summary>
        /// Generic deserialization of a class using XML attributes from an XML Node
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <param name="ser"></param>
        /// <returns></returns>
        private static T ConvertNode<T>(XmlNode node, XmlSerializer ser) where T : class
        {
            MemoryStream stm = new MemoryStream();

            StreamWriter stw = new StreamWriter(stm);
            stw.Write(node.OuterXml);
            stw.Flush();

            stm.Position = 0;

            // Creating a serializer is expensive so allow us to re-use an existing one.
            if (ser == null)
                ser = new XmlSerializer(typeof(T));

            T result = (ser.Deserialize(stm) as T);

            return result;
        }

    }

    public static class GzipCompression
    {

        public static byte[] Compress(byte[] data)
        {
            MemoryStream output = new MemoryStream();
            GZipStream gzip = new GZipStream(output,
                              CompressionMode.Compress, true);
            gzip.Write(data, 0, data.Length);
            gzip.Close();
            return output.ToArray();
        }

        public static byte[] Decompress(byte[] data)
        {
            MemoryStream input = new MemoryStream();
            input.Write(data, 0, data.Length);
            input.Position = 0;
            GZipStream gzip = new GZipStream(input,
                              CompressionMode.Decompress, true);
            MemoryStream output = new MemoryStream();
            byte[] buff = new byte[64];
            int read = -1;
            read = gzip.Read(buff, 0, buff.Length);
            while (read > 0)
            {
                output.Write(buff, 0, read);
                read = gzip.Read(buff, 0, buff.Length);
            }
            gzip.Close();
            return output.ToArray();
        }
    }
}