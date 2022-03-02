using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public GameObject windowPopUpGameObject;
    [HideInInspector]
    public Animator windowPopUpAnimator;
    public GameObject warningMessageGameObject;
    [HideInInspector]
    public Animator warningMessageAnimator;
    public GameObject timerGameObject;
    [HideInInspector]

    public Animator timerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        windowPopUpAnimator = windowPopUpGameObject.GetComponent<Animator>();
        warningMessageAnimator = warningMessageGameObject.GetComponent<Animator>();
        timerAnimator = timerGameObject.GetComponent<Animator>();
    }
}
