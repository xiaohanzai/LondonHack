using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerIllustrator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text0;
    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private TextMeshProUGUI text2;

    private Vector3 answer0;
    private Vector3 answer1;
    private Vector3 answer2;

    [SerializeField] private float speed = 1f;

    [SerializeField] private Transform gridObjectTransform;
    [SerializeField] private Illustrator illustratorPrefab;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private GameObject cannonObject;

    [Header("Listening to")]
    [SerializeField] private Vector3sEventChannelSO answersSetEventChannel;
    [SerializeField] private GlobalParametersEventChannelSO globalParametersEventChannel;

    private Vector3 gridOrigin;
    private float cellSize;

    private void Start()
    {
        globalParametersEventChannel.OnEvtRaised += SetParameters;
        answersSetEventChannel.OnEvtRaised += SetAnswers;
    }

    private void OnDestroy()
    {
        globalParametersEventChannel.OnEvtRaised -= SetParameters;
        answersSetEventChannel.OnEvtRaised -= SetAnswers;
    }

    private void SetParameters(Vector3 o, float s)
    {
        gridOrigin = o;
        cellSize = s;
    }

    private void SetAnswers(Vector3 v0, Vector3 v1, Vector3 v2)
    {
        text0.text = "(" + v0.x.ToString() + ", " + v0.z.ToString();
        text1.text = "(" + v1.x.ToString() + ", " + v1.z.ToString();
        text2.text = "(" + v2.x.ToString() + ", " + v2.z.ToString();
        if (v0.y != 0)
        {
            text0.text += ", " + v0.y.ToString();
            text1.text += ", " + v1.y.ToString();
            text2.text += ", " + v2.y.ToString();
        }
        text0.text += ")";
        text1.text += ")";
        text2.text += ")";

        answer0 = v0 * cellSize + gridOrigin;
        answer1 = v1 * cellSize + gridOrigin;
        answer2 = v2 * cellSize + gridOrigin;
    }

    public void ShowAnswer(int i)
    {
        Vector3 dest;
        if (i == 0)
        {
            dest = answer0;
        }
        else if (i == 1)
        {
            dest = answer1;
        }
        else
        {
            dest = answer2;
        }

        StartCoroutine(Co_MoveIllustratorAndShoot(dest));
    }

    IEnumerator Co_MoveIllustratorAndShoot(Vector3 target)
    {
        Illustrator illustrator = Instantiate(illustratorPrefab, gridObjectTransform);
        illustrator.transform.position = gridOrigin;
        LineRenderer lineRenderer = illustrator.GetLineRenderer();
        lineRenderer.SetPosition(0, gridOrigin);

        if (target == answer0)
        {
            illustrator.ApplyRightAnswerMat();
        }
        else
        {
            illustrator.ApplyWrongAnswerMat();
        }

        bool moveInX = true;
        bool moveInY = false;
        bool moveInZ = false;

        // Move in the x direction
        while (moveInX)
        {
            illustrator.transform.position = Vector3.MoveTowards(illustrator.transform.position, new Vector3(target.x, illustrator.transform.position.y, illustrator.transform.position.z), speed * Time.deltaTime);
            illustrator.transform.LookAt(illustrator.transform.position * 2);
            cannonObject.transform.LookAt(illustrator.transform.position * 2);
            lineRenderer.SetPosition(1, illustrator.transform.position);
            if (Mathf.Approximately(illustrator.transform.position.x, target.x))
            {
                moveInX = false;
                moveInZ = true;
            }
            yield return null;
        }

        // Move in the z direction
        while (moveInZ)
        {
            illustrator.transform.position = Vector3.MoveTowards(illustrator.transform.position, new Vector3(illustrator.transform.position.x, illustrator.transform.position.y, target.z), speed * Time.deltaTime);
            illustrator.transform.LookAt(illustrator.transform.position * 2);
            cannonObject.transform.LookAt(illustrator.transform.position * 2);
            lineRenderer.SetPosition(1, illustrator.transform.position);
            if (Mathf.Approximately(illustrator.transform.position.z, target.z))
            {
                moveInZ = false;
                moveInY = true;
            }
            yield return null;
        }

        // Move in the y direction
        while (moveInY)
        {
            illustrator.transform.position = Vector3.MoveTowards(illustrator.transform.position, new Vector3(illustrator.transform.position.x, target.y, illustrator.transform.position.z), speed * Time.deltaTime);
            lineRenderer.SetPosition(1, illustrator.transform.position);
            illustrator.transform.LookAt(illustrator.transform.position * 2);
            cannonObject.transform.LookAt(illustrator.transform.position * 2);
            if (Mathf.Approximately(illustrator.transform.position.y, target.y))
            {
                moveInY = false;
            }
            yield return null;
        }

        Projectile projectile = Instantiate(projectilePrefab, gridOrigin, Quaternion.identity);
        projectile.Shoot(target);

        float waitTime = 2f;
        if (target == answer0)
        {
            waitTime = 7f;
        }
        yield return new WaitForSeconds(waitTime);
        Destroy(illustrator.gameObject);
    }
}
