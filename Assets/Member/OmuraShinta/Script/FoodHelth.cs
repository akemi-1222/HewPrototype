using Unity.VisualScripting;
using UnityEngine;



public class FoodHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public enum FoodSize 
    {
        XXS,
        XS,
        S,
        M,
        L,
        XL,
        XXL,
    }

    [SerializeField]
    private FoodSize foodSize;

    //‘Ì—Í
    private int Hp;

    void Start()
    {
        switch (foodSize)
        {
            case FoodSize.XXS:
                Hp = 1;
                break;
        }

        switch (foodSize)
        {
            case FoodSize.XS:
                Hp = 3;
                break;
        }

        switch (foodSize)
        {
            case FoodSize.S:
                Hp = 5;
                break;
        }

        switch (foodSize)
        {
            case FoodSize.M:
                Hp = 9;
                break;
        }

        switch (foodSize)
        {
            case FoodSize.L:
                Hp = 15;
                break;
        }

        switch (foodSize)
        {
            case FoodSize.XL:
                Hp = 17;
                break;
        }

        switch (foodSize)
        {
            case FoodSize.XXL:
                Hp = 20;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {

        Hp -= damage;
        if(Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
