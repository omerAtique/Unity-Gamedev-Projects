using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _health;

    public int Health
    {
        get{
            return _health;
        }
        set{
            _health = value;
        }
    }
    private int _power;

    public int Power
    {
        get{
            return _power;
        }
        set{
            _power = value;
        }
    }
    private string _name;

    public string Name{
        get{
            return _name;
        }
        set{
            _name = value;
        }
    }

    public Player(){ }

    public Player(int health, int power, string name)
    {
        this._health = health;
        this._power = power;
        this._name = name;

        
    }
    public void info()
    {
        Debug.Log("Health is = " + Health);
        Debug.Log("Power is = " + Power);
        Debug.Log("Name is = " + Name);
    }
    public void attack()
    {
        Debug.Log("Eye of the cards.");
    }
}//class
