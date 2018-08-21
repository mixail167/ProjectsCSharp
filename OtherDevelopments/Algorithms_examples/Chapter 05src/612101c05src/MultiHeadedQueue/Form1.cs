using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiHeadedQueue
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Simulation parameters.
        private int Time, NumTellers, MinArrival, MaxArrival, MinDuration, MaxDuration;

        // The queue.
        private Queue<Customer> CustomerQueue;

        // Record which cutomer is being servced by each teller.
        private Customer[] TellerServing;

        // The time the next customer will arrive.
        private int NextArrivalTime;

        // Track average wait time.
        private int NumServed, TotalWaitTime;

        // The next customer ID.
        private int NextId;

        // Used to generate random times.
        private Random Rand = new Random();

        // Start or stop the simulation.
        private void startButton_Click(object sender, EventArgs e)
        {
            if (startButton.Text == "Start") StartSimulation();
            else StopSimulation();
        }

        // Start the simulation.
        private void StartSimulation()
        {
            NumTellers = (int)numTellersNumericUpDown.Value;
            MinArrival = (int)minArrivalNumericUpDown.Value;
            MaxArrival = (int)maxArrivalNumericUpDown.Value;
            MinDuration = (int)minDurationNumericUpDown.Value;
            MaxDuration = (int)maxDurationNumericUpDown.Value;
            TellerServing = new Customer[NumTellers];
            CustomerQueue = new Queue<Customer>();
            NumServed = 0;
            TotalWaitTime = 0;
            NextId = 1;

            Time = 0;
            NextArrivalTime = 1;

            // Display the current situation.
            ShowCustomers();

            startButton.Text = "Stop";
            minuteTimer.Interval = (int)(1000 / speedNumericUpDown.Value);
            minuteTimer.Enabled = true;
        }

        // Stop the simulation.
        private void StopSimulation()
        {
            startButton.Text = "Start";
            minuteTimer.Enabled = false;
        }

        // Show the current situation.
        private void ShowCustomers()
        {
            // Show the customers in the queue.
            string txt = "";
            foreach (Customer customer in CustomerQueue)
                txt += " " + customer.Id.ToString();
            if (txt.Length > 0) txt = txt.Substring(1);
            queueTextBox.Text = txt;

            // Show the customers being served.
            txt = "";
            foreach (Customer customer in TellerServing)
            {
                if (customer == null) txt += " --";
                else txt += " " + customer.Id.ToString();
            }
            if (txt.Length > 0) txt = txt.Substring(1);
            tellersTextBox.Text = txt;

            // Show elapsed time.
            int hours = Time / 60;
            int mins = Time - hours * 60;
            timeTextBox.Text = string.Format("{0} hours, {1} mins", hours, mins);

            // Show the average wait time.
            if (NumServed == 0) averageWaitTextBox.Clear();
            else
            {
                float elapsed = (float)TotalWaitTime / (float)NumServed;
                int minutes = (int)elapsed;
                int seconds = (int)((elapsed - minutes) * 60);
                averageWaitTextBox.Text = string.Format("{0} min, {1} sec", minutes, seconds);
            }
        }

        // A minute has passed.
        private void minuteTimer_Tick(object sender, EventArgs e)
        {
            // Tick.
            Time++;

            // See if we need to create a new customer.
            if (NextArrivalTime <= Time)
            {
                // Create a customer.
                Customer customer = new Customer();
                customer.CreatedTime = Time;
                customer.Id = NextId;
                NextId++;

                // Add the customer to the queue.
                CustomerQueue.Enqueue(customer);

                // See when to add the next customer.
                NextArrivalTime = Time +
                    Rand.Next(MinArrival, MaxArrival + 1);
            }

            // Process the tellers.
            for (int i = 0; i < NumTellers; i++)
            {
                // If this teller is serving someone,
                // see if that customer is done.
                if ((TellerServing[i] != null) &&
                    (TellerServing[i].FinishedTime <= Time))
                    TellerServing[i] = null;

                // If this teller is available,
                // move a customer here.
                if ((TellerServing[i] == null) &&
                    (CustomerQueue.Count > 0))
                {
                    // This teller isn't busy. Move a customer here.
                    Customer customer = CustomerQueue.Dequeue();
                    TellerServing[i] = customer;

                    // Set the customer's finish time.
                    customer.FinishedTime = Time + Rand.Next(MinDuration, MaxDuration + 1);

                    // Record the customer's wait time.
                    TotalWaitTime += Time - customer.CreatedTime;
                    NumServed++;
                }
            }

            // Display the new situation.
            ShowCustomers();
        }
    }
}
