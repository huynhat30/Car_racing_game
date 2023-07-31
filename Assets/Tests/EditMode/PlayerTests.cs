using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTest
{

    [Test]
    public void Player1Test()
    {
        int playerNumber = 1;

        switch (playerNumber)
        {
            case 1:
                Assert.AreEqual(1, playerNumber);
                break;
            case 2:
                Assert.AreEqual(2, playerNumber);
                break;
        }


    }

    [Test]
    public void Player2Test()
    {
        int playerNumber = 2;

        switch (playerNumber)
        {
            case 1:
                Assert.AreEqual(1, playerNumber);
                break;
            case 2:
                Assert.AreEqual(2, playerNumber);
                break;
        }

    }





}