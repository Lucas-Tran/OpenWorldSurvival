using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour {


    [SerializeField] GameObject IHasHealth;
    [SerializeField] private Image bar;

    private IHasHealth tree;

    private void Awake() {
        tree = IHasHealth.GetComponent<IHasHealth>();

        if (tree == null) {
            Debug.LogError("IHasHealth GameObject does not have component with IHasHealth");
        }
    }

    private void Start() {
        tree.OnHealthChanged += Tree_OnHealthChanged;
    }

    private void Tree_OnHealthChanged(object sender, IHasHealth.OnHealthChangedEventArgs e) {
        bar.fillAmount = e.healthNormalized;
    }


}