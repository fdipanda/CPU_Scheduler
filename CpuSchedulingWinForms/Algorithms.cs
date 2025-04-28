using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpuSchedulingWinForms
{
    public static class Algorithms
    {
        public static void fcfsAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            int npX2 = np * 2;

            double[] bp = new double[np];
            double[] wtp = new double[np];
            string[] output1 = new string[npX2];
            double twt = 0.0, awt; 
            int num;

            DialogResult result = MessageBox.Show("First Come First Serve Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (num = 0; num <= np - 1; num++)
                {
                    //MessageBox.Show("Enter Burst time for P" + (num + 1) + ":", "Burst time for Process", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    //Console.WriteLine("\nEnter Burst time for P" + (num + 1) + ":");

                    string input =
                    Microsoft.VisualBasic.Interaction.InputBox("Enter Burst time: ",
                                                       "Burst time for P" + (num + 1),
                                                       "",
                                                       -1, -1);

                    bp[num] = Convert.ToInt64(input);

                    //var input = Console.ReadLine();
                    //bp[num] = Convert.ToInt32(input);
                }

                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        wtp[num] = 0;
                    }
                    else
                    {
                        wtp[num] = wtp[num - 1] + bp[num - 1];
                        MessageBox.Show("Waiting time for P" + (num + 1) + " = " + wtp[num], "Job Queue", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                awt = twt / np;
                MessageBox.Show("Average waiting time for " + np + " processes" + " = " + awt + " sec(s)", "Average Awaiting Time", MessageBoxButtons.OK, MessageBoxIcon.None);

                double totalBurstTime = 0;
                for (int i = 0; i < np; i++)
                {
                    totalBurstTime += bp[i];
                }


                double totalSimulationTime = wtp[np - 1] + bp[np - 1];

                double cpuUtilization = (totalBurstTime / totalSimulationTime) * 100.0;

                double throughput = (double)np / totalSimulationTime;

                // Generate the report
                Algorithms.GenerateReport("FCFS", awt, awt + 5, cpuUtilization, throughput);
            }
            else if (result == DialogResult.No)
            {
                //this.Hide();
                //Form1 frm = new Form1();
                //frm.ShowDialog();
            }
        }

        public static void sjfAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);

            double[] bp = new double[np];
            double[] wtp = new double[np];
            double[] p = new double[np];
            double twt = 0.0, awt; 
            int x, num;
            double temp = 0.0;
            bool found = false;

            DialogResult result = MessageBox.Show("Shortest Job First Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (num = 0; num <= np - 1; num++)
                {
                    string input =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    bp[num] = Convert.ToInt64(input);
                }
                for (num = 0; num <= np - 1; num++)
                {
                    p[num] = bp[num];
                }
                for (x = 0; x <= np - 2; x++)
                {
                    for (num = 0; num <= np - 2; num++)
                    {
                        if (p[num] > p[num + 1])
                        {
                            temp = p[num];
                            p[num] = p[num + 1];
                            p[num + 1] = temp;
                        }
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (p[num] == bp[x] && found == false)
                            {
                                wtp[num] = 0;
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time:", MessageBoxButtons.OK, MessageBoxIcon.None);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                bp[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (p[num] == bp[x] && found == false)
                            {
                                wtp[num] = wtp[num - 1] + p[num - 1];
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time", MessageBoxButtons.OK, MessageBoxIcon.None);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                bp[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                MessageBox.Show("Average waiting time for " + np + " processes" + " = " + (awt = twt / np) + " sec(s)", "Average waiting time", MessageBoxButtons.OK, MessageBoxIcon.Information);


                double totalBurstTime = 0;
                for (int i = 0; i < np; i++)
                {
                    totalBurstTime += bp[i];
                }

                double totalSimulationTime = wtp[np - 1] + bp[np - 1];
                double cpuUtilization = (totalBurstTime / totalSimulationTime) * 100.0;
                double throughput = (double)np / totalSimulationTime;

                Algorithms.GenerateReport("SJF", awt, awt + 5, cpuUtilization, throughput); 



            }
        }

        public static void priorityAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);

            DialogResult result = MessageBox.Show("Priority Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                double[] bp = new double[np];
                double[] wtp = new double[np + 1];
                int[] p = new int[np];
                int[] sp = new int[np];
                int x, num;
                double twt = 0.0;
                double awt;
                int temp = 0;
                bool found = false;
                for (num = 0; num <= np - 1; num++)
                {
                    string input =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    bp[num] = Convert.ToInt64(input);
                }
                for (num = 0; num <= np - 1; num++)
                {
                    string input2 =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter priority: ",
                                                           "Priority for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    p[num] = Convert.ToInt16(input2);
                }
                for (num = 0; num <= np - 1; num++)
                {
                    sp[num] = p[num];
                }
                for (x = 0; x <= np - 2; x++)
                {
                    for (num = 0; num <= np - 2; num++)
                    {
                        if (sp[num] > sp[num + 1])
                        {
                            temp = sp[num];
                            sp[num] = sp[num + 1];
                            sp[num + 1] = temp;
                        }
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (sp[num] == p[x] && found == false)
                            {
                                wtp[num] = 0;
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time", MessageBoxButtons.OK);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                temp = x;
                                p[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (sp[num] == p[x] && found == false)
                            {
                                wtp[num] = wtp[num - 1] + bp[temp];
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time", MessageBoxButtons.OK);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                temp = x;
                                p[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                MessageBox.Show("Average waiting time for " + np + " processes" + " = " + (awt = twt / np) + " sec(s)", "Average waiting time", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Console.WriteLine("\n\nAverage waiting time: " + (awt = twt / np));
                //Console.ReadLine();

                double totalBurstTime = 0;
                for (int i = 0; i < np; i++)
                {
                    totalBurstTime += bp[i];
                }

                double totalSimulationTime = wtp[np - 1] + bp[np - 1];
                double cpuUtilization = (totalBurstTime / totalSimulationTime) * 100.0;
                double throughput = (double)np / totalSimulationTime;

                Algorithms.GenerateReport("Priority", awt, awt + 5, cpuUtilization, throughput);  

            }
            else
            {
                //this.Hide();
            }
        }

        public static void roundRobinAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            int i, counter = 0;
            double total = 0.0;
            double timeQuantum;
            double waitTime = 0, turnaroundTime = 0;
            double averageWaitTime, averageTurnaroundTime;
            double[] arrivalTime = new double[10];
            double[] burstTime = new double[10];
            double[] temp = new double[10];
            int x = np;

            DialogResult result = MessageBox.Show("Round Robin Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (i = 0; i < np; i++)
                {
                    string arrivalInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter arrival time: ",
                                                               "Arrival time for P" + (i + 1),
                                                               "",
                                                               -1, -1);

                    arrivalTime[i] = Convert.ToInt64(arrivalInput);

                    string burstInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                               "Burst time for P" + (i + 1),
                                                               "",
                                                               -1, -1);

                    burstTime[i] = Convert.ToInt64(burstInput);

                    temp[i] = burstTime[i];
                }
                string timeQuantumInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter time quantum: ", "Time Quantum",
                                                               "",
                                                               -1, -1);

                timeQuantum = Convert.ToInt64(timeQuantumInput);
                Helper.QuantumTime = timeQuantumInput;

                for (total = 0, i = 0; x != 0;)
                {
                    if (temp[i] <= timeQuantum && temp[i] > 0)
                    {
                        total = total + temp[i];
                        temp[i] = 0;
                        counter = 1;
                    }
                    else if (temp[i] > 0)
                    {
                        temp[i] = temp[i] - timeQuantum;
                        total = total + timeQuantum;
                    }
                    if (temp[i] == 0 && counter == 1)
                    {
                        x--;
                        //printf("nProcess[%d]tt%dtt %dttt %d", i + 1, burst_time[i], total - arrival_time[i], total - arrival_time[i] - burst_time[i]);
                        MessageBox.Show("Turnaround time for Process " + (i + 1) + " : " + (total - arrivalTime[i]), "Turnaround time for Process " + (i + 1), MessageBoxButtons.OK);
                        MessageBox.Show("Wait time for Process " + (i + 1) + " : " + (total - arrivalTime[i] - burstTime[i]), "Wait time for Process " + (i + 1), MessageBoxButtons.OK);
                        turnaroundTime = (turnaroundTime + total - arrivalTime[i]);
                        waitTime = (waitTime + total - arrivalTime[i] - burstTime[i]);                        
                        counter = 0;
                    }
                    if (i == np - 1)
                    {
                        i = 0;
                    }
                    else if (arrivalTime[i + 1] <= total)
                    {
                        i++;
                    }
                    else
                    {
                        i = 0;
                    }
                }
                averageWaitTime = Convert.ToInt64(waitTime * 1.0 / np);
                averageTurnaroundTime = Convert.ToInt64(turnaroundTime * 1.0 / np);
                MessageBox.Show("Average wait time for " + np + " processes: " + averageWaitTime + " sec(s)", "", MessageBoxButtons.OK);
                MessageBox.Show("Average turnaround time for " + np + " processes: " + averageTurnaroundTime + " sec(s)", "", MessageBoxButtons.OK);
               
                double totalBurstTime = 0;
                for (int j = 0; j < np; j++)
                {
                    totalBurstTime += burstTime[i];
                }

                double totalSimulationTime = total;
                double cpuUtilization = (totalBurstTime / totalSimulationTime) * 100.0;
                double throughput = (double)np / totalSimulationTime;

                Algorithms.GenerateReport("Round Robin", averageWaitTime, averageTurnaroundTime, cpuUtilization, throughput);


            }
        }

        public static void srtfAlgorithm(string userInput)
        {
            int np = Convert.ToInt32(userInput);
            int[] at = new int[np];
            int[] bt = new int[np];
            int[] rt = new int[np];
            int completed = 0, currentTime = 0, minIndex = -1;
            int minRemainingTime = int.MaxValue;
            int totalWaitingTime = 0, totalTurnaroundTime = 0;
            bool found = false;

            DialogResult result = MessageBox.Show("Shortest Remaining Time First Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                for (int i = 0; i < np; i++)
                {
                    at[i] = Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox($"Enter Arrival Time for Process {i + 1}:"));
                    bt[i] = Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox($"Enter Burst Time for Process {i + 1}:"));
                    rt[i] = bt[i];
                }

                while (completed != np)
                {
                    found = false;
                    minRemainingTime = int.MaxValue;

                    for (int i = 0; i < np; i++)
                    {
                        if (at[i] <= currentTime && rt[i] > 0 && rt[i] < minRemainingTime)
                        {
                            minRemainingTime = rt[i];
                            minIndex = i;
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        currentTime++;
                        continue;
                    }

                    rt[minIndex]--;

                    if (rt[minIndex] == 0)
                    {
                        completed++;
                        int finishTime = currentTime + 1;
                        int turnaroundTime = finishTime - at[minIndex];
                        int waitingTime = turnaroundTime - bt[minIndex];

                        totalWaitingTime += waitingTime;
                        totalTurnaroundTime += turnaroundTime;

                        MessageBox.Show($"Process {minIndex + 1}\nWaiting Time: {waitingTime}\nTurnaround Time: {turnaroundTime}", "", MessageBoxButtons.OK);
                    }
                    currentTime++;
                }

                double avgWaitingTime = (double)totalWaitingTime / np;
                double avgTurnaroundTime = (double)totalTurnaroundTime / np;

                MessageBox.Show($"Average Waiting Time = {avgWaitingTime:F2} sec(s)\nAverage Turnaround Time = {avgTurnaroundTime:F2} sec(s)", "SRTF Scheduling Result", MessageBoxButtons.OK);

                double totalBurstTime = 0;
                for (int i = 0; i < np; i++)
                {
                    totalBurstTime += bt[i];
                }

                double totalSimulationTime = currentTime; 
                double cpuUtilization = (totalBurstTime / totalSimulationTime) * 100.0;
                double throughput = (double)np / totalSimulationTime;

                Algorithms.GenerateReport("Shortest Remaining Time First Scheduling", avgWaitingTime, avgTurnaroundTime, cpuUtilization, throughput);

            }
        }




        public static void hrrnAlgorithm(string userInput)
        {
            int np = Convert.ToInt32(userInput);
            int[] at = new int[np];
            int[] bt = new int[np];
            bool[] completed = new bool[np];
            int currentTime = 0, completedCount = 0;
            double totalWaitingTime = 0, totalTurnaroundTime = 0;

            DialogResult result = MessageBox.Show("Highest Response Ratio Next Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                for (int i = 0; i < np; i++)
                {
                    at[i] = Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox($"Enter Arrival Time for Process {i + 1}:"));
                    bt[i] = Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox($"Enter Burst Time for Process {i + 1}:"));
                    completed[i] = false;
                }

                while (completedCount != np)
                {
                    double highestResponseRatio = -1;
                    int selectedProcess = -1;

                    for (int i = 0; i < np; i++)
                    {
                        if (at[i] <= currentTime && !completed[i])
                        {
                            double responseRatio = (double)(currentTime - at[i] + bt[i]) / bt[i];
                            if (responseRatio > highestResponseRatio)
                            {
                                highestResponseRatio = responseRatio;
                                selectedProcess = i;
                            }
                        }
                    }

                    if (selectedProcess == -1)
                    {
                        currentTime++;
                        continue;
                    }

                    int startTime = currentTime;
                    currentTime += bt[selectedProcess];
                    int finishTime = currentTime;
                    int turnaroundTime = finishTime - at[selectedProcess];
                    int waitingTime = turnaroundTime - bt[selectedProcess];

                    totalWaitingTime += waitingTime;
                    totalTurnaroundTime += turnaroundTime;
                    completed[selectedProcess] = true;
                    completedCount++;

                    MessageBox.Show($"Process {selectedProcess + 1}\nWaiting Time: {waitingTime}\nTurnaround Time: {turnaroundTime}", "", MessageBoxButtons.OK);
                }

                double avgWaitingTime = totalWaitingTime / np;
                double avgTurnaroundTime = totalTurnaroundTime / np;

                MessageBox.Show($"Average Waiting Time = {avgWaitingTime:F2} sec(s)\nAverage Turnaround Time = {avgTurnaroundTime:F2} sec(s)", "HRRN Scheduling Result", MessageBoxButtons.OK);

                double totalBurstTime = 0;
                for (int i = 0; i < np; i++)
                {
                    totalBurstTime += bt[i];
                }

                double totalSimulationTime = currentTime;  // after the loop ends
                double cpuUtilization = (totalBurstTime / totalSimulationTime) * 100.0;
                double throughput = (double)np / totalSimulationTime;

                Algorithms.GenerateReport("Highest Response Ratio Next Scheduling", avgWaitingTime, avgTurnaroundTime, cpuUtilization, throughput);

            }
        }

        public static void GenerateReport(string algorithmName, double avgWaitingTime, double avgTurnaroundTime, double cpuUtilization, double throughput)
        {
            try
            {
                
                string directoryPath = Path.Combine(Application.StartupPath, "Reports");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string fileName = $"{algorithmName}_Report_{timestamp}.txt";
                string filePath = Path.Combine(directoryPath, fileName);

                // Write the contents of the report
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("CPU Scheduling Simulation Report");
                    writer.WriteLine("--------------------------------");
                    writer.WriteLine($"Algorithm: {algorithmName}");
                    writer.WriteLine($"Average Waiting Time: {avgWaitingTime:F2} sec(s)");
                    writer.WriteLine($"Average Turnaround Time: {avgTurnaroundTime:F2} sec(s)");
                    writer.WriteLine($"CPU Utilization: {cpuUtilization:F2} %");
                    writer.WriteLine($"Throughput: {throughput:F4} processes/sec");
                    writer.WriteLine();
                    writer.WriteLine($"Report Generated On: {DateTime.Now}");
                }

                
                MessageBox.Show($"Report generated successfully!\n\nSaved to:\n{filePath}",
                    "Report Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
               
                MessageBox.Show($"An error occurred while generating the report:\n\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

