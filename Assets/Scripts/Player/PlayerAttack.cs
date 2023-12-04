using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private float currAmmo;
    [SerializeField] private float maxAmmo;

    [SerializeField] private float attackCooldown;
    [SerializeField] private float reloadTime;

    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI ammoReloadText;
    [SerializeField] private Image ammoIcon;

    [SerializeField] private SpikeBall SpikeBallPrefab;

    private AudioController audioController;

    private bool canAttack;
    private bool isReloading;
    private float iconStartingWidth;
    // Start is called before the first frame update
    void Awake()
    {
        currAmmo = maxAmmo;
        canAttack = true;
        iconStartingWidth = ammoIcon.rectTransform.sizeDelta.x;
        audioController = FindObjectOfType(typeof(AudioController)) as AudioController;
    }

    // Update is called once per frame
    void Update()
    {
        currAmmo = Mathf.Clamp(currAmmo, 0, maxAmmo);
        UpdateAmmoUI();
        if (currAmmo == 0) Reload();
    }

    private void UpdateAmmoUI()
    {
        if (isReloading) return;
        ammoText.text = "" + (int)currAmmo;
        Vector2 _temp = ammoIcon.rectTransform.sizeDelta;
        _temp.x = (currAmmo / maxAmmo) * iconStartingWidth;
        ammoIcon.rectTransform.sizeDelta = _temp;
    }

    public void Attack()
    {
        StartCoroutine(AttackAction());
    }

    public void Reload()
    {
        if (currAmmo < 3) StartCoroutine(ReloadAction());
    }

    private IEnumerator AttackAction()
    {
        if(currAmmo >= 1 && canAttack && !isReloading)
        {
            audioController.AttackSFX();
            canAttack = false;
            currAmmo--;
            SpikeBall spikeBall = Instantiate(SpikeBallPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }
    }

    private IEnumerator ReloadAction()
    {
        isReloading = true;
        float reloadDuration = reloadTime * (maxAmmo - currAmmo);
        audioController.ReloadSFX();
        currAmmo = 3;
        ammoIcon.enabled = false;
        ammoText.enabled = false;
        ammoReloadText.text = "Reloading..";
        yield return new WaitForSeconds(reloadDuration);
        ammoIcon.enabled = true;
        ammoText.enabled = true;
        ammoReloadText.text = "";
        isReloading = false;
    }
}
