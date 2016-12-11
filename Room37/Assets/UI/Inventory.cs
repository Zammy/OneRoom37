using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour 
{
    [SerializeField]
    SymbolView[] symbolViews;



    public void DisplayWithWitness(WitnessInteraction witness)
    {
        if (_currentlyWitness != null)
        {
            _currentlyWitness.IsInteracting = false;
        }
        this.gameObject.SetActive(true);
        _currentlyWitness = witness;
        _currentlyWitness.IsInteracting = true;
    }

    public void SymbolSelected(SymbolView symbolView)
    {
        char newSymbol = _currentlyWitness.Story[symbolView.SymbolText.text[0]];
        if (!symbolViews[1].gameObject.activeSelf)
        {
            symbolViews[1].SymbolText.text = newSymbol.ToString();
            symbolViews[1].gameObject.SetActive(true);
        }
        else if (!symbolViews[2].gameObject.activeSelf)
        {
            symbolViews[2].SymbolText.text = newSymbol.ToString();
            symbolViews[2].gameObject.SetActive(true);
        }
        else
        {
            symbolView.SymbolText.text = newSymbol.ToString();
        }
    }

    WitnessInteraction _currentlyWitness;
}
