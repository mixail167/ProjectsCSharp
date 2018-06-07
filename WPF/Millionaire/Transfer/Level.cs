using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transfer
{
    class Level
    {
        string question;
        string[] answers;
        string trueAnswer;

        public Level()
        {
            answers = new string[4];
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

        public override string ToString()
        {
            return string.Format("{0}#{1}#{2}#{3}#{4}#{5}", question, answers[0], answers[1], answers[2], answers[3], trueAnswer);
        }
    }
}
