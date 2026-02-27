using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] IngredientData ingredient; // Optionnel (null pour le verre)

    private Vector3 offset;
    private Vector3 oldPos;
    private bool dragging = false;

    // NOUVEAU - Pour identifier si c'est le verre
    private CocktailManager cocktailManager;

    void Start()
    {
        // Check si cet objet EST le verre
        cocktailManager = GetComponent<CocktailManager>();
    }

    private void Update()
    {
        if (dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        oldPos = transform.position;
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;

        Collider2D[] hits = Physics2D.OverlapPointAll(transform.position);

        foreach (var hit in hits)
        {
            // SI C'EST LE VERRE qui est draggé
            if (cocktailManager != null)
            {
                // Check seulement la poubelle
                Bin bin = hit.GetComponent<Bin>();
                if (bin != null)
                {
                    bin.DeleteIngredients();
                    transform.position = oldPos;
                    return;
                }
            }
            // SI C'EST UN INGRÉDIENT qui est draggé
            else if (ingredient != null)
            {
                // Check le verre
                CocktailManager glass = hit.GetComponent<CocktailManager>();
                if (glass != null)
                {
                    glass.AddIngredient(ingredient);
                    transform.position = oldPos;
                    return;
                }

                // Check la poubelle
                Bin bin = hit.GetComponent<Bin>();
                if (bin != null)
                {
                    bin.DeleteIngredients();
                    transform.position = oldPos;
                    return;
                }
            }
        }

        // Retour position
        transform.position = oldPos;
    }
}