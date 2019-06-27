using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace OTBSolution.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize()]
        public void TestInit()
        {
            Program.PendingJobs = new Dictionary<string, Job>();
            Program.ResultSequence = string.Empty;
        }

        [TestMethod]
        public void TestMethod1()
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

            var result = Program.JobsHelper(jobList);
            Assert.AreEqual("fedcba", result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            List<Job> jobList = new List<Job>();
            jobList.Add(new Job("a"));
            var result = Program.JobsHelper(jobList);
            Assert.AreEqual("a", result);
        }

        [TestMethod]
        public void TestMethod3()
        {
            List<Job> jobList = new List<Job>();
            Job a = new Job("a");
            Job b = new Job("b");
            Job c = new Job("c");
            b.Dependency = c;
            jobList.AddRange(new Job[] { a, b, c });

            var result = Program.JobsHelper(jobList);
            Assert.AreEqual("acb", result);
        }

        [TestMethod]
        public void TestMethod4()
        {
            List<Job> jobList = new List<Job>();
            Job a = new Job("a");
            Job b = new Job("b");
            Job c = new Job("c");
            Job d = new Job("d");
            Job e = new Job("e");
            Job f = new Job("f");
            b.Dependency = c;
            c.Dependency = f;
            d.Dependency = a;
            e.Dependency = b;
            jobList.AddRange(new Job[] { a, b, c, d, e, f });

            var result = Program.JobsHelper(jobList);
            Assert.AreEqual("afcbde", result);
        }

        [TestMethod]
        public void TestMethod5()
        {
            List<Job> jobList = new List<Job>();
            Job a = new Job("a");
            Job b = new Job("b");
            Job c = new Job("c");
            c.Dependency = c;
            jobList.AddRange(new Job[] { a, b, c });

            var result = Program.JobsHelper(jobList);
            Assert.AreEqual(Constants.Error_Self_Dependency, result);
        }

        [TestMethod]
        public void TestMethod6()
        {
            List<Job> jobList = new List<Job>();
            Job a = new Job("a");
            Job b = new Job("b");
            Job c = new Job("c");
            Job d = new Job("d");
            Job e = new Job("e");
            Job f = new Job("f");
            b.Dependency = c;
            c.Dependency = f;
            d.Dependency = a;
            f.Dependency = b;
            jobList.AddRange(new Job[] { a, b, c, d, e, f });

            var result = Program.JobsHelper(jobList);
            Assert.AreEqual(Constants.Error_Circular_Dependency, result);
        }

        [TestMethod]
        public void TestMethod7()
        {
            List<Job> jobList = new List<Job>();
            var result = Program.JobsHelper(jobList);
            Assert.AreEqual(Constants.Error_No_Jobs, result);
        }
    }
}
