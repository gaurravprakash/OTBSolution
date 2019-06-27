using System;
using System.Collections.Generic;
using System.Linq;

namespace OTBSolution
{
    /// <summary>
    /// Sample class representing a Job.
    /// </summary>
    public class Job
    {
        /// <summary>
        /// The Job id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The job dependency i.e. the job on which this job is dependent upon.
        /// </summary>
        public Job Dependency { get; set; }

        /// <summary>
        /// Indicates wether or not the job has been executed.
        /// </summary>
        public bool IsExecuted { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">The job id.</param>
        public Job(string id)
        {
            this.Id = id;
        }
    }

    /// <summary>
    /// Driver class for console application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// A collection of jobs that are dependent on some other job.
        /// </summary>
        public static Dictionary<string, Job> PendingJobs = new Dictionary<string, Job>();

        /// <summary>
        /// An id based string representation of the final ordering of jobs.
        /// </summary>
        public static string ResultSequence = string.Empty;

        /// <summary>
        /// Program entry point.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            List<Job> jobList = new List<Job>();

            Job a = new Job("a");
            Job b = new Job("b");
            Job c = new Job("c");
            Job d = new Job("d");
            Job e = new Job("e");
            Job f = new Job("f");

            a.Dependency = b;
            b.Dependency = c;
            c.Dependency = d;
            d.Dependency = e;
            e.Dependency = f;

            jobList.AddRange(new Job[] { a, b, c, d, e, f });
            Console.WriteLine("\n" + JobsHelper(jobList));
            Console.ReadKey();
        }

        /// <summary>
        /// Wrapper method for iterating over the Job list.
        /// </summary>
        /// <param name="jobs">The list of Jobs.</param>
        public static string JobsHelper(List<Job> jobs)
        {
            string result = string.Empty;
            try
            {
                //Base case if there are no input jobs.
                if (jobs.Count() == 0)
                {
                    return Constants.Error_No_Jobs;
                }
                //Process each input job one by one
                foreach (Job job in jobs)
                {
                    CheckDependencyAndExecuteJob(job);
                }
                return ResultSequence;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        static void CheckDependencyAndExecuteJob(Job job)
        {
            //If job is dependent upon itself.
            if (job.Dependency != null && job.Dependency.Id == job.Id)
            {
                throw new Exception(Constants.Error_Self_Dependency);
            }
            //Case for circular dependency.
            if (PendingJobs.ContainsKey(job.Id))
            {
                throw new Exception(Constants.Error_Circular_Dependency);
            }
            //If job is dependent on some other job.
            if (job.Dependency != null)
            {
                PendingJobs.Add(job.Id, job);
                CheckDependencyAndExecuteJob(job.Dependency);
            }
            //If job does not have any dependency, pass it to handler for execution.
            else if (job.Dependency == null)
            {
                ExecuteJob(job);
            }
            //Execute the pending jobs that were waiting for some other jobs to finish.
            for (int i = PendingJobs.Count - 1; i >= 0; i--)
            {
                var pendingJob = PendingJobs.ElementAt(i);
                ExecuteJob(pendingJob.Value);
                PendingJobs.Remove(pendingJob.Key);
            }
        }

        /// <summary>
        /// A job execution handler that simulates execution of a given job.
        /// </summary>
        /// <param name="job">The input job.</param>
        static void ExecuteJob(Job job)
        {
            if (!job.IsExecuted)
            {
                //I am just simulating the job execution by printing a message, this will be handled by a real job execution handler in reality.
                Console.WriteLine("Executing job : " + job.Id);
                job.IsExecuted = true;
                ResultSequence += job.Id;
            }
        }
    }
}
