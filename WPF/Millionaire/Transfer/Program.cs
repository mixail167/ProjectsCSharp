using System;
using Crypt;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace Transfer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Level> level1 = new List<Level>();
            List<Level> level2 = new List<Level>();
            List<Level> level3 = new List<Level>();
            List<Record> records = new List<Record>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                ReadData(cmd, "level1", level1);
                ReadData(cmd, "level2", level2);
                ReadData(cmd, "level3", level3);
                ReadData(cmd, records);
                connection.Close();
                try
                {
                    DelAttributes("data.bin");
                    using (FileStream fileStream = File.Create("data.bin"))
                    {
                        string[] temp = new string[3];
                        temp[0] = string.Join<Level>("%", level1.ToArray());
                        temp[1] = string.Join<Level>("%", level2.ToArray());
                        temp[2] = string.Join<Level>("%", level3.ToArray());
                        byte[] bytes = AesCrypt.EncryptStringToBytes(string.Join(";", temp), Encoding.ASCII.GetBytes("zxcvqwerasdfqazx"), Encoding.ASCII.GetBytes("qazxcvbnmlpoiuyt"));
                        fileStream.Write(bytes, 0, bytes.Length);
                    }
                    SetAttributes("data.bin");

                    DelAttributes("records.bin");
                    using (FileStream fileStream = File.Create("records.bin"))
                    {
                        byte[] bytes = AesCrypt.EncryptStringToBytes(string.Join<Record>("%", records.ToArray()), Encoding.ASCII.GetBytes("zxcvqwerasdfqazx"), Encoding.ASCII.GetBytes("qazxcvbnmlpoiuyt"));
                        fileStream.Write(bytes, 0, bytes.Length);
                    }
                    SetAttributes("records.bin");
                   
                    byte[] fileData;
                    using (FileStream fileStream = File.OpenRead("data.bin"))
                    {
                        fileData = new byte[fileStream.Length];
                        fileStream.Read(fileData, 0, (int)fileStream.Length);
                    }
                    byte[] checkSum;
                    using (MD5 md5 = new MD5CryptoServiceProvider())
                    {
                        checkSum = md5.ComputeHash(fileData);
                    }

                    DelAttributes("data.md5");
                    using (FileStream fileStream = File.Create("data.md5"))
                    {
                        fileStream.Write(checkSum, 0, checkSum.Length);
                    }
                    SetAttributes("data.md5");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            }
            catch (SqlException ex)
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                    connection.Close();
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void DelAttributes(string fileName)
        {
            try
            {
                FileAttributes fileAttributes = File.GetAttributes(fileName);
                if (fileAttributes.HasFlag(FileAttributes.ReadOnly))
                    File.SetAttributes(fileName, fileAttributes & ~FileAttributes.ReadOnly);
            }
            catch
            {

            }
        }

        static void SetAttributes(string fileName)
        {
            try
            {
                File.SetAttributes(fileName, File.GetAttributes(fileName) | FileAttributes.ReadOnly);
            }
            catch
            {

            }
        }

        static void ReadData(SqlCommand cmd, string BDName, List<Level> list)
        {
            cmd.CommandText = "Select Question, Answer1, Answer2, Answer3, Answer4, TrueAnswer from " + BDName;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Level level = new Level();
                level.Question = reader.GetValue(0).ToString();
                level.Answers[0] = reader.GetValue(1).ToString();
                level.Answers[1] = reader.GetValue(2).ToString();
                level.Answers[2] = reader.GetValue(3).ToString();
                level.Answers[3] = reader.GetValue(4).ToString();
                level.TrueAnswer = reader.GetValue(5).ToString();
                list.Add(level);
            }
            reader.Close();
        }

        static void ReadData(SqlCommand cmd, List<Record> list)
        {
            cmd.CommandText = "Select Name, Record from Records";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Record record = new Record();
                record.Name = reader.GetValue(0).ToString();
                record.Score = Convert.ToInt32(reader.GetValue(1));
                list.Add(record);
            }
            reader.Close();
        }       
    }
}
