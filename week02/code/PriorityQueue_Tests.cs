using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Create a queue with three people and ensure that they are dequeued in the correct order.
    // Scenario: Create a queue with: Bob (2), Tim (3) and Sue (1)
    // Expected Result: Tim, Bob, Sue
    // Defect(s) Found: 
    // Error: Expected tim but returned bob. The queue is not recognizing the priority of the people within the list.
    public void TestPriorityQueue_IsWorkingQueue()
    {
        var bob = new PriorityItem("Bob", 2);
        var sue = new PriorityItem("Sue", 1);
        var tim = new PriorityItem("Tim", 3);
        
        var expectedResult = new List<PriorityItem> {tim, bob, sue};

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue(bob.Value, bob.Priority);
        priorityQueue.Enqueue(sue.Value, sue.Priority);
        priorityQueue.Enqueue(tim.Value, tim.Priority);

        Assert.AreEqual(expectedResult[0].Value, priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add three people and then add two more people with higher/same priority. 
    // Expected Result: Tim first and then Jill.
    // Defect(s) Found: 
    // Error: No errors found. The queue is working as expected.
    public void TestPriorityQueue_CanAddHighLowPriorities()
    {
        var bob = new PriorityItem("Bob", 2);
        var sue = new PriorityItem("Sue", 1);
        var tim = new PriorityItem("Tim", 3);
        var george = new PriorityItem("George", 3);
        var jill = new PriorityItem("Jill", 4);
        
        var firstexpectedResult = new List<PriorityItem> {tim, bob, sue};

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue(bob.Value, bob.Priority);
        priorityQueue.Enqueue(sue.Value, sue.Priority);
        priorityQueue.Enqueue(tim.Value, tim.Priority);

        Assert.AreEqual(firstexpectedResult[0].Value, priorityQueue.Dequeue());

        priorityQueue.Enqueue(george.Value, george.Priority);
        priorityQueue.Enqueue(jill.Value, jill.Priority);

        var secondexpectedResult = new List<PriorityItem> {jill, george, bob, sue};
        Assert.AreEqual(secondexpectedResult[0].Value, priorityQueue.Dequeue());


    }

    [TestMethod]
    // Scenario: Create a queue without any people and check if the queue is empty.
    // Expected Result: Error The queue is empty.
    // Defect(s) Found: It indeed throws an error when the queue is empty - no errors.
    public void TestPriorityQueue_IsEmpty()
    {
        var userQueue = new PriorityQueue();

        Assert.ThrowsException<InvalidOperationException>(() => userQueue.Dequeue());
        
    }

    // Add more test cases as needed below.
}