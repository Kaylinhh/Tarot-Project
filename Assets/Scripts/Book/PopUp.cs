using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{

    [SerializeField] GameObject book;
    [SerializeField] bool displayBook;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopUpBook()
    {
        if (displayBook)
        {
            book.SetActive(true);
            displayBook = false;
        }
        else 
        {
            book.SetActive(false);    
            displayBook = true;
        }

    }
}
