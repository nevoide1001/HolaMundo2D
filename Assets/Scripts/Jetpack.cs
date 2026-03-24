using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    #region Base
    // Energia del Jetpack;
    public float energy
    {
        get
        {
            return Energy;
        }
        set
        {
            Energy = Mathf.Clamp(value,0,maxEnergy);
        }
    }
    private Rigidbody2D targetRB;

    [SerializeField] private Rigidbody2D target;
    [SerializeField] public float maxEnergy;
    [SerializeField] private float energyFlyingRatio;
    [SerializeField] private float energyRegenerationRatio;
    [SerializeField] private float horizontalForce;
    [SerializeField] private float flyForce;
    [SerializeField] public float Energy;
    private bool flying { get; set; }
    


    public enum Direction { Left, Right }
    #endregion

    #region Methods
    void Start()
{
    if (target == null) 
    {
        target = GetComponent<Rigidbody2D>();
        maxEnergy = 100f;
        Energy = 100f;
        
        if (target == null)
        {
            Debug.LogError("No se encontró un Rigidbody2D en el Player.");
        }
    }

    energy = maxEnergy;
}

    private void Awake()
    {
        targetRB = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (flying)
        {
            Fly();
        }

        if(Mathf.Abs(targetRB.velocity.y)<0.1f)
        {
            Regenerate(0.5f);
        }
    }

    public void flyUp()
    {
        flying = true;
    }

    public void stopFlying()
    {
        flying = false;
    }

    public void Regenerate(float amount)
    {
        energy += amount * energyRegenerationRatio;
        energy = Mathf.Clamp(energy, 0, maxEnergy);
    }

    public void flyHorizontal(Direction direction)
    {
        if (direction == Direction.Left)
        {
            target.AddForce(Vector3.left * horizontalForce);
        }
        else
        {
            target.AddForce(Vector3.right * horizontalForce);
        }
    }
    #endregion

    #region PrivateMethods
    private void Fly()
    {
        if (energy > 0)
        {
            target.AddForce(Vector2.up * flyForce);
            energy -= energyFlyingRatio;
        }
        else
        {
            flying = false;
        }
    }
    #endregion
}