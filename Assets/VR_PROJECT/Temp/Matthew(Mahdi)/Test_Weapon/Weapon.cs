using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Weapon : MonoBehaviour
{
    public InputActionReference triggerAndFireInputAction = null;
    public InputActionReference magOutWithButtonInputAction = null;
    public GrabStatus grabStatus ;
    public WeaponTypes weaponType;    
    
    [Header("Muzzle flash")]
    private float destroyTimer = 2f;
    public GameObject muzzleFlashPrefab;
    public GameObject muzzleFlashLocation; 
    
    
    [Header("Sound effect")]
    public AudioSource source;
    public AudioClip fireSound;
    public AudioClip targetHitSound;
    public AudioClip wallHitSound;
    public AudioClip magInSound;
    public AudioClip magOutSound;
    public AudioClip noAmmoShootSound;
    public AudioClip SliderReload;    
    private void playTargetHit() => source.PlayOneShot(targetHitSound);
    private void playWallHit() => source.PlayOneShot(wallHitSound);
    private void playChik() => source.PlayOneShot(noAmmoShootSound);
    private void playSlider() => source.PlayOneShot(SliderReload);
    
    [Header("Animations")]
    public Animator recoilAnimator; // float param -> "RecoilValue"
    public Animator magAnimator; // Play "MagIn" and "MagOut"
    public Animator shootingModeAnimator; // bool param "IsAutomatic"
    public Animator sliderAnimator; // Trigger "SlideBack" and "SlideForward" and "SlideBackAndForward"
    
    [Header("Shooting config")]
    public ParticleSystem impactSystem;
    public ParticleSystem bloodimpactSystem;
    public TrailRenderer bulletTrail;
    
    [Header("Magazine")]
    public string magName = "";
    public int currentBullets = 15;

    public float SoundDistance = 10;
    public bool haveMagInside = false;
    public GameObject magPrefab;
    public GameObject magThatIsInside;
    public GameObject magReceiver;
    public bool magOutWithSecondHand = false;
    public bool haveBulletInBarrel = false;
    
    [Header("Automatic")]
    public bool supportsAutomatic = false;
    public float fireRate = 10; // shot per sec
    public float one_hand_recoil_power = .5f; // 1 is Max
    public float two_hand_recoil_power = .1f; // 1 is Max
    public bool isAutomatic = false; // default

    private float timeBetweenShots; // in millis and calculated on Start()
    private float recoilValue = 0; // between 0 and 1
    private float lastShootTime = 0;

    private void Awake()
    {
        triggerAndFireInputAction.action.started += Fire;
        magOutWithButtonInputAction.action.started += magReleaseWithButtonLogic;
    }

    private void OnDestroy()
    {
        triggerAndFireInputAction.action.started -= Fire;
        magOutWithButtonInputAction.action.started -= magReleaseWithButtonLogic;
    }

    private void Start()
    {
        grabStatus = GetComponent<GrabStatus>();
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire Excuted");
        if (grabStatus.isGrabing)
        {
            lastShootTime = Time.time;
            if(!haveBulletInBarrel) return;
            haveBulletInBarrel = false;
            
            if(currentBullets > 0 && !haveBulletInBarrel){
                haveBulletInBarrel = true;
                currentBullets--;
            }
            

            
            MuzzleFlash();
            RaycastShotLine();
        }
    }

    private void RaycastShotLine()
    {
        // hit target logic
        RaycastHit hit;
        if(Physics.Raycast(muzzleFlashLocation.transform.position, muzzleFlashLocation.transform.forward, out hit)){

            if (hit.transform != null)
            {
                StartCoroutine(shootEffect(hit));
            }

        } else {
            // miss shot to sky
            //MissShotHandler.missShot();
        }
    }

    private void MuzzleFlash()
    {
        // muzzle flash
        if (muzzleFlashPrefab){
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, muzzleFlashLocation.transform.position, muzzleFlashLocation.transform.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }
    }

    private IEnumerator shootEffect(RaycastHit hit)
    {
        
        float time = 0;
        var hitTimePoint = hit.transform.position;

        // Trail (shooting line light)
        if (hit.distance > 5)
        {
            var trail = Instantiate(bulletTrail, muzzleFlashLocation.transform.position, Quaternion.identity);
            Vector3 startPosition = trail.transform.position;

            while (time < 1)
            {
                trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
                time = Mathf.Clamp(time + (Time.deltaTime / trail.time) * 2, 0, 1); // x2 speed trail
                yield return null;
            }

            Destroy(trail.gameObject, 0.3f);
        }

        // Hit sound
        HitSound(hit);

        // Impact
        StartCoroutine(Impact(hit));

        yield return null;
    }

    private void HitSound(RaycastHit hit)
    {
        // You can add logic to detect multiple surfaces to play other sound effects
        playWallHit();
    }

    private IEnumerator Impact(RaycastHit hit)
    {
        float time = 0;
        var hitTimePoint = hit.transform.position;

        var impact = Instantiate(impactSystem, hit.point, Quaternion.LookRotation(hit.normal));

        // Env impact
        time = 0;
        var animationTime = 1000;

        while (time < 1)
        {
            if (hit.transform != null)
            {
                var offset = hit.transform.position - hitTimePoint;
                if (impact != null) { impact.transform.position += offset; }
            }

            time += Time.deltaTime / animationTime;
            hitTimePoint = hit.transform.position;
            yield return null;
        }

        Destroy(impact.gameObject, 0);
    }    
    
    // private void magLogic(){
    //     magReleaseLogic();
    //     magInLogic();
    // }    
    private void magReleaseLogic(InputAction.CallbackContext context){
        // if(!haveMagInside) return;
        // if(magOutWithSecondHand){
        //     magReleaseWithSecondHandLogic();
        // } else {
        //    // magReleaseWithButtonLogic();
        // }
        void magReleaseWithSecondHandLogic(){
            // bool rightGrab = HandPresence.getRightGrip() > 0.5;
            // bool leftGrab = HandPresence.getLeftGrip() > 0.5;
            //
            //
            // if (lastLeftGrabForMagIn != leftGrab && leftGrab){ // if calls one time on grab release
            //
            //     var distance = Vector3.Distance(magReceiver.transform.position, HandPresence.getLeftHandPos());
            //     if(
            //         HandPresence.leftHandHolden == null &&
            //         HandPresence.rightHandHolden.name == transform.parent.gameObject.name &&
            //         distance < 0.07
            //     ){
            //         playMagOut();
            //     }
            // }

            // if (lastRightGrabForMagIn != rightGrab && rightGrab){ // if calls one time on grab release
            //     var distance = Vector3.Distance(magReceiver.transform.position, HandPresence.getRightHandPos());
            //     if(
            //         HandPresence.rightHandHolden == null &&
            //         HandPresence.leftHandHolden.name == transform.parent.gameObject.name &&
            //         distance < 0.07
            //     ){
            //         playMagOut();
            //     }
            // }
            //
            // lastLeftGrabForMagIn = leftGrab;
            // lastRightGrabForMagIn = rightGrab;
        }
    }        
    void magReleaseWithButtonLogic(InputAction.CallbackContext context){
        if (!grabStatus.isGrabing)return;
        if(!haveMagInside) return;
        if (magOutWithSecondHand) return;
        // if right controller buttons pressed and i grab weapon with my right hand then release Mag 
        playMagOut();
    }
    public void playMagOut(){
        haveMagInside = false;
        magAnimator.Play("MagOut");
        StartCoroutine(setTimeOut(()=>{onMagOut();},.1f));
        source.PlayOneShot(magOutSound);
        //sliderAnimator.Play("SlideBack");
    }    
    public void onMagOut(){

        magThatIsInside.SetActive(false);
        GameObject tempMag = Instantiate(magPrefab, magThatIsInside.transform.position, magThatIsInside.transform.rotation) as GameObject;
        tempMag.AddComponent<Rigidbody>();
        tempMag.GetComponent<MagScript>().init(currentBullets);
        currentBullets = 0 ; 
        Destroy(tempMag,10);
        
    }


    
    
    


    public enum WeaponTypes
    {
        Pistol,
        Rifle,
        Sniper,
        Shotgun
    }    public delegate void Callback();

    public static IEnumerator setTimeOut(Callback callback, float time){
        yield return new WaitForSeconds(time);
        callback();
    }
}
