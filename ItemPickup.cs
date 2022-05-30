using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    [Header("Textu ce apare cand iei in mana itemu (gen key, flashlight, battery)")]
    [SerializeField] public string text;
    [Header("Chestia ce o sa fie pusa pe off")]
    public Text itemText;
    public Animator animator;
    public string pickedAnimation;

    // Start is called before the first frame update
    public void PickedItem()
    {
        itemText.text = text;
        animator.Play(pickedAnimation);
    }
}
