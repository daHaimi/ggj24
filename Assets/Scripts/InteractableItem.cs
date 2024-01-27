using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace Assets.Scripts
{
    /// <summary>
    /// Interactable that gives the player an item.
    /// </summary>
    public class InteractableItem : Interactable
    {
        public ItemSo Item;
        public bool DestroyOnInteract = true;
        public UnityEvent OnInteraction;

        public override void Interact()
        {
            GameController.Instance.AddItem(Item);

            OnInteraction.Invoke();

            if (DestroyOnInteract)
                Destroy(gameObject);
        }
    }
}
