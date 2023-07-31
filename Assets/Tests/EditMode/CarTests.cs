using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CarTests
{
    [Test]
    public void CarSteertingTest()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = 1;
        inputVector.y = 1;

        Assert.AreEqual(1, inputVector.x);
        Assert.AreNotEqual(0, inputVector.x);
    }


    [Test]
    public void CarVelocityTest()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = 1;
        inputVector.y = 1;

        Assert.AreEqual(1, inputVector.y);
        Assert.AreNotEqual(0, inputVector.x);
    }
}
