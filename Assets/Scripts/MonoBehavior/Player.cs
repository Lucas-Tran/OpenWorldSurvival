using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IItemParent {


    public event EventHandler OnHit;

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedObjectChangedEventArgs> OnSelectedObjectChanged;
    public class OnSelectedObjectChangedEventArgs : EventArgs {
        public IInteractable selectedObject;
    }

    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform playerHandPointTransform;

    private Item heldItem;
    private int heldItemInventoryIndex;

    private IInteractable selectedObject;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        GameInput.Instance.OnInteract += GameInput_OnInteract;
    }

    private void GameInput_OnInteract(object sender, System.EventArgs e) {
        if (selectedObject != null) {
            selectedObject.Interact(this);
        }
    }

    private void Update() {
        HandleInteractions();
        HandleMovement();

        for (int i = 0; i < 10; i++) {
            if (Input.GetKey(i.ToString())) {
                SetHeldItemInventoryIndex(i);
            }
        }
    }

    private void HandleInteractions() {
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, interactDistance)) {
            if (raycastHit.transform.TryGetComponent<IInteractable>(out IInteractable newSelectedObject)) {
                SetSelectedObject(newSelectedObject);
            } else {
                SetSelectedObject(null);
            }
        } else {
            SetSelectedObject(null);
        }
    }

    private void HandleMovement() {
        Vector2 inputVector = GameInput.Instance.GetMoveVectorNormalized();
        if (inputVector != Vector2.zero) {

            Vector3 moveDirection = transform.right * inputVector.x + transform.forward * inputVector.y;


            float moveDistance = moveSpeed * Time.deltaTime;


            float playerHeight = 2f;
            float playerRadius = 1.5f;
            bool canMove = !Physics.CapsuleCast(transform.position, transform.position + (playerHeight * Vector3.up), playerRadius, moveDirection, moveDistance);
            if (!canMove) {
                // Cannot move in all directions

                // Attempt only X movement
                Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + (playerHeight * Vector3.up), playerRadius, moveDirectionX, moveDistance);
                if (canMove) {
                    // Can move in the X direction
                    moveDirection = moveDirectionX;
                } else {
                    // Cannot move in X direction

                    // Attempt only Z movement
                    Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z);
                    canMove = !Physics.CapsuleCast(transform.position, transform.position + (playerHeight * Vector3.up), playerRadius, moveDirectionZ, moveDistance);
                    if (canMove) {
                        // Can move in the Z direction
                        moveDirection = moveDirectionZ;
                    } else {
                        // Cannot move in Z direction
                        // Cannot move in any direction
                    }
                }
            } else {
                // Can move all directions
            }

            if (canMove) {
                transform.position += moveDirection * moveDistance;
            }
        }
    }

    private void SetSelectedObject(IInteractable selectedObject) {
        if (this.selectedObject != selectedObject) {
            this.selectedObject = selectedObject;
            OnSelectedObjectChanged?.Invoke(this, new OnSelectedObjectChangedEventArgs {
                selectedObject = selectedObject
            });
        }
    }

    public void Hit() {
        OnHit?.Invoke(this, EventArgs.Empty);
    }

    public Transform GetItemHoldPoint() {
        return playerHandPointTransform;
    }

    public Item GetItem() {
        return heldItem;
    }

    public void SetItem(Item item) {
        heldItem = item;
    }

    public bool HasItem() {
        return heldItem != null;
    }

    public void ClearItem() {
        heldItem = null;
    }

    public bool HasItemWithCatagory(ItemSO.ItemCatagories itemCatagory) {
        return HasItem() && GetItem().GetItemSO().catagory == itemCatagory;
    }

    public void SetHeldItemInventoryIndex(int heldItemInventoryIndex) {
        this.heldItemInventoryIndex = heldItemInventoryIndex;
        if (HasItem()) {
            heldItem.DestroySelf();
        }

        ItemSO itemSO = InventoryManager.Instance.GetItemSOArray()[heldItemInventoryIndex];

        if (itemSO != null) {
            Transform newHeldItem = Instantiate(itemSO.prefab);
            newHeldItem.GetComponent<Item>().SetItemParent(this);
        }
    }


}