using UnityEngine;

public class DirectionChooser : MonoBehaviour
{
    public static int ChoosenDirection { get; private set; } = -1;
    public static bool HasChoosenDirection { get; private set; } = false;

    private void Awake()
    {
        Player.OnMultipleRouts += StartChoosingProcess;
    }

    private void OnDestroy()
    {
        Player.OnMultipleRouts -= StartChoosingProcess;
    }

    private void OnEnable()
    {
        ChoosenDirection = -1;
        HasChoosenDirection = false;
    }

    public void StartChoosingProcess()
    {
        ChoosenDirection = -1;
        HasChoosenDirection = false;
    }

    public void ChooseDirection(int directionIndex)
    {
        ChoosenDirection = directionIndex;
        HasChoosenDirection = true;
        this.gameObject.SetActive(false);
    }

    public static void ResetParams()
    {
        ChoosenDirection = -1;
        HasChoosenDirection = false;
    }
}
