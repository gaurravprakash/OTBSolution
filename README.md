# Initial thoughts

As I read the problem statement, an approach started to build up in my mind.

I knew I need to iterate over a collection of Jobs, and for each item I need to check if there is some dependency. If so, I need to remember the current job, and process it's dependency first. If another dependency is encountered, I need to again remember the current job, and proceed to resolve it's dependency first, and so on.

The very first thing that came to my mind is to create a data structure representing the Job, and then use a stack to remember the jobs while processing their dependencies. But then I had to cater for circular dependency as well, so I decided another approach and completed the solution.

# Approach

I am using a sample data structure to represent a Job entity. It has three basic properties - Id, IsExecuted(execution status), and a dependency - which is again of type Job. I am using a Dictionary object to remember the jobs that have some dependency, since I need to first process their dependencies and then, come back to execute them. I could have used any type of collection here but the reason I am using Dictionary is because Dictionary type in .Net uses HashTables internally, and the hash operations like put, get, delete etc. are optimized to be linear.

# Algorithm

The algorithm is simple. For each job in the job list - 
1. Check if dependency id is same as job id. </br>
  a. If yes, throw error. If no, go to step 2.
2. Check if pending jobs hashtable already contains the current job id. </br>
  a. If yes, throw error for circular dependency. If no, go to step 3.
3. Check if job has no dependency, and IsExecuted is false. </br>
  a. If yes, execute the job.
4. Iterate over pending jobs collection in reverse order. </br>
  a. If IsExecuted is false, execute the job. </br>
  b. Remove this item from pending jobs collection.
5. Go to step 1.

# Job execution handler

I am using simulating a job execution handler by simply printing the job id. The program also returns a string built by concatenating the job id's in the order of their expected execution.

# Unit tests

Unit test cases have been added under the project "OTBSolution.Tests".

# How to run

Open the "OTBSolution.sln" file in Visual Studio and click on start button. Test cases can be run using the "Test Explorer" window of Visual Studio. Please feel free to share your thoughts/improvement suggestions.

