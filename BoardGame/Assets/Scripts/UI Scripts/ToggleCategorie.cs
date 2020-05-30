using UnityEngine;

public class ToggleCategorie : MonoBehaviour
{
    [SerializeField] private Categories _categorie;

    public Categories Categorie => _categorie;
}
