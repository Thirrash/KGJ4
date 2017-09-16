using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlidePanelController : MonoBehaviour
{
    public GameObject FirstImage;
    public GameObject FirstText;

    public GameObject SecondImage;
    public GameObject SecondText;

    public GameObject ThirdImage;
    public GameObject ThirdText;

    public GameObject FourthImage;
    public GameObject FourthText;

    public GameObject FifthImage;
    public GameObject FifthText;

    public GameObject SixthImage;
    public GameObject SixthText;

    public float TimeBetweenSlides = 2.5f;
    public int CurrentSlide = 1;

    // Use this for initialization
    void Start() {
        
    }

    private void OnEnable() {
        StartCoroutine(NextSlide());
    }

    // Update is called once per frame
    void Update() {

    }

    private IEnumerator NextSlide() {
        yield return new WaitForSeconds(TimeBetweenSlides);
        float timer = 0.0f;
        SecondImage.SetActive(true);
        SecondText.SetActive(true);

        while (timer < 1.0f) {
            FirstImage.GetComponent<Image>().material.color = new Color(1.0f, 1.0f, 1.0f, (1.0f - timer) / 1.0f);
            FirstText.GetComponentInChildren<Image>().material.color = new Color(0.0f, 0.0f, 0.0f, (1.0f - timer) / 1.0f);
            FirstText.GetComponentInChildren<Text>().material.color = new Color(1.0f, 1.0f, 1.0f, (1.0f - timer) / 1.0f);
            timer += Time.deltaTime;

            SecondImage.GetComponent<Image>().material.color = new Color(1.0f, 1.0f, 1.0f, (timer) / 1.0f);
            SecondText.GetComponentInChildren<Image>().material.color = new Color(0.0f, 0.0f, 0.0f, (timer) / 1.0f);
            SecondText.GetComponentInChildren<Text>(true).material.color = new Color(1.0f, 1.0f, 1.0f, (timer) / 1.0f);
            yield return null;
        }

        FirstImage.SetActive(false);
        FirstText.SetActive(false);
        yield return new WaitForSeconds(TimeBetweenSlides);
        timer = 0.0f;
        ThirdImage.SetActive(true);
        ThirdText.SetActive(true);

        while (timer < 1.0f) {
            SecondImage.GetComponent<Image>().material.color = new Color(1.0f, 1.0f, 1.0f, (1.0f - timer) / 1.0f);
            SecondText.GetComponentInChildren<Image>().material.color = new Color(0.0f, 0.0f, 0.0f, (1.0f - timer) / 1.0f);
            SecondText.GetComponentInChildren<Text>().material.color = new Color(1.0f, 1.0f, 1.0f, (1.0f - timer) / 1.0f);
            timer += Time.deltaTime;

            ThirdImage.GetComponent<Image>().material.color = new Color(1.0f, 1.0f, 1.0f, (timer) / 1.0f);
            ThirdText.GetComponentInChildren<Image>().material.color = new Color(0.0f, 0.0f, 0.0f, (timer) / 1.0f);
            ThirdText.GetComponentInChildren<Text>(true).material.color = new Color(1.0f, 1.0f, 1.0f, (timer) / 1.0f);
            yield return null;
        }

        SecondImage.SetActive(false);
        SecondText.SetActive(false);
        yield return new WaitForSeconds(TimeBetweenSlides);
        timer = 0.0f;
        FourthImage.SetActive(true);
        FourthText.SetActive(true);

        while (timer < 1.0f) {
            ThirdImage.GetComponent<Image>().material.color = new Color(1.0f, 1.0f, 1.0f, (1.0f - timer) / 1.0f);
            ThirdText.GetComponentInChildren<Image>(true).material.color = new Color(0.0f, 0.0f, 0.0f, (1.0f - timer) / 1.0f);
            ThirdText.GetComponentInChildren<Text>(true).material.color = new Color(1.0f, 1.0f, 1.0f, (1.0f - timer) / 1.0f);
            timer += Time.deltaTime;

            FourthImage.GetComponent<Image>().material.color = new Color(1.0f, 1.0f, 1.0f, (timer) / 1.0f);
            FourthText.GetComponentInChildren<Image>(true).material.color = new Color(0.0f, 0.0f, 0.0f, (timer) / 1.0f);
            FourthText.GetComponentInChildren<Text>(true).material.color = new Color(1.0f, 1.0f, 1.0f, (timer) / 1.0f);
            yield return null;
        }

        ThirdText.SetActive(false);
        ThirdImage.SetActive(false);
        yield return new WaitForSeconds(TimeBetweenSlides);
        timer = 0.0f;
        FifthImage.SetActive(true);
        FifthText.SetActive(true);

        while (timer < 1.0f) {
            FourthImage.GetComponent<Image>().material.color = new Color(1.0f, 1.0f, 1.0f, (1.0f - timer) / 1.0f);
            FourthText.GetComponentInChildren<Image>(true).material.color = new Color(0.0f, 0.0f, 0.0f, (1.0f - timer) / 1.0f);
            FourthText.GetComponentInChildren<Text>(true).material.color = new Color(1.0f, 1.0f, 1.0f, (1.0f - timer) / 1.0f);
            timer += Time.deltaTime;

            FifthImage.GetComponent<Image>().material.color = new Color(1.0f, 1.0f, 1.0f, (timer) / 1.0f);
            FifthText.GetComponentInChildren<Image>().material.color = new Color(0.0f, 0.0f, 0.0f, (timer) / 1.0f);
            FifthText.GetComponentInChildren<Text>(true).material.color = new Color(1.0f, 1.0f, 1.0f, (timer) / 1.0f);
            yield return null;
        }

        FourthImage.SetActive(false);
        FourthText.SetActive(false);
        yield return new WaitForSeconds(TimeBetweenSlides);
        timer = 0.0f;
        SixthImage.SetActive(true);
        SixthText.SetActive(true);

        while (timer < 1.0f) {
            FifthImage.GetComponent<Image>().material.color = new Color(1.0f, 1.0f, 1.0f, (1.0f - timer) / 1.0f);
            FifthText.GetComponentInChildren<Image>(true).material.color = new Color(0.0f, 0.0f, 0.0f, (1.0f - timer) / 1.0f);
            FifthText.GetComponentInChildren<Text>(true).material.color = new Color(1.0f, 1.0f, 1.0f, (1.0f - timer) / 1.0f);
            timer += Time.deltaTime;

            SixthImage.GetComponent<Image>().material.color = new Color(1.0f, 1.0f, 1.0f, (timer) / 1.0f);
            SixthText.GetComponentInChildren<Image>().material.color = new Color(0.0f, 0.0f, 0.0f, (timer) / 1.0f);
            SixthText.GetComponentInChildren<Text>(true).material.color = new Color(1.0f, 1.0f, 1.0f, (timer) / 1.0f);
            yield return null;
        }

        FifthImage.SetActive(false);
        FifthText.SetActive(false);
        yield return new WaitForSeconds(TimeBetweenSlides);

        SceneManager.LoadScene(1);

    }
}
