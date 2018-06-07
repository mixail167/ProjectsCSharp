using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Millionaire
{
    public class Data
    {
        string question;
        string[] answers;
        string trueAnswer;

        public Data()
        {
            answers = new string[4];
        }

        public Data(string[] answers, string trueAnswer)
        {
            this.answers = answers;
            this.trueAnswer = trueAnswer;
        }
        public string Question
        {
            get { return question; }
            set { question = value; }
        }

        public string[] Answers
        {
            get { return answers; }
            set { answers = value; }
        }

        public string TrueAnswer
        {
            get { return trueAnswer; }
            set { trueAnswer = value; }
        }

        //public override string ToString()
        //{
        //    return string.Format("{0}#{1}#{2}#{3}#{4}#{5}", question, answers[0], answers[1], answers[2], answers[3], trueAnswer);
        //}

        public static Data Parse(string[] parts)
        {
            try
            {
                if (parts.Length == 6)
                {
                    Data data = new Data();
                    data.question = parts[0];
                    data.answers[0] = parts[1];
                    data.answers[1] = parts[2];
                    data.answers[2] = parts[3];
                    data.answers[3] = parts[4];
                    data.trueAnswer = parts[5];
                    return data;
                }
                else throw new Exception();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Random()
        {
            List<string> temp = new List<string>(answers);
            Random r = new Random();
            for (int i = 0; i < answers.Length; i++)
            {
                int index = r.Next(0, temp.Count);
                answers[i] = temp[index];
                temp.RemoveAt(index);
            }
        } 
    }
}
