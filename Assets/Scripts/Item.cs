using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private Canvas ItemCanvas;

    public Button itemHeightPlus;
    public Button itemHeightMinus;

    public Button itemRotatePlus;
    public Button itemRotateMinus;

    public InputField itemLength;
    public InputField itemHeight;
    public InputField itemWidth;
    public Button submitNewDiamentions;

    private InputField iLength;
    private InputField iWidth;
    private InputField iHeight;

    public Button delete;

    public GameObject root;


    private Vector3 targetSize;



    private void Start()
    {
        SetRootPosition();
    }

    void OnMouseDown()
    {
        ItemCanvas = GameObject.FindGameObjectWithTag("ItemCanvas").GetComponent<Canvas>();

        foreach (Transform child in GameObject.FindGameObjectWithTag("ItemCanvas").transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        Button heightPlus = Instantiate(itemHeightPlus);
        Button heightMinus = Instantiate(itemHeightMinus);

        heightPlus.transform.SetParent(ItemCanvas.transform);
        heightMinus.transform.SetParent(ItemCanvas.transform);

        heightPlus.GetComponent<Button>().onClick.AddListener(() => ChangeHeightPlus());
        heightMinus.GetComponent<Button>().onClick.AddListener(() => ChangeHeightMinus());


        Button rotatePlus = Instantiate(itemRotatePlus);
        Button rotateMinus = Instantiate(itemRotateMinus);

        rotatePlus.transform.SetParent(ItemCanvas.transform);
        rotateMinus.transform.SetParent(ItemCanvas.transform);

        rotatePlus.GetComponent<Button>().onClick.AddListener(() => RotatePlus());
        rotateMinus.GetComponent<Button>().onClick.AddListener(() => RotateMinus());


        Button iDelete = Instantiate(delete);

        iDelete.transform.SetParent(ItemCanvas.transform);

        iDelete.GetComponent<Button>().onClick.AddListener(() => Delete());



        iLength = Instantiate(itemLength);
        iWidth = Instantiate(itemWidth);
        iHeight = Instantiate(itemHeight);
        Button submit = Instantiate(submitNewDiamentions);

        iLength.transform.SetParent(ItemCanvas.transform);
        iWidth.transform.SetParent(ItemCanvas.transform);
        iHeight.transform.SetParent(ItemCanvas.transform);
        submit.transform.SetParent(ItemCanvas.transform);

        submit.GetComponent<Button>().onClick.AddListener(() => ChangeItemSize());

        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        root.GetComponent<ItemRoot>().OnMouseDown();

        iLength.GetComponent<InputField>().text = targetSize.x.ToString();
        iWidth.GetComponent<InputField>().text = targetSize.z.ToString();
        iHeight.GetComponent<InputField>().text = targetSize.y.ToString();
    }

    void OnMouseDrag()
    {
        root.GetComponent<ItemRoot>().OnMouseDrag();   
    }

    private void OnMouseUp()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<Rigidbody>().isKinematic = true;
        SetRootPosition();
    }

    public void ChangeHeightPlus()
    {
        var position = transform.position;
        position.y += 100;
        transform.position = position;
    }

    public void ChangeHeightMinus()
    {
        var position = transform.position;
        position.y -= 100;
        transform.position = position;
    }

    public void RotatePlus()
    {
        if(tag == "Bed")
            transform.Rotate(new Vector3(0, 0, 90), Space.Self);
        else
            transform.Rotate(new Vector3(0, 90, 0), Space.Self);
    }

    public void RotateMinus()
    {
        if (tag == "Bed")
            transform.Rotate(new Vector3(0, 0, -90), Space.Self);
        else
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
    }

    public void ChangeItemSize()
    {
        string length = iLength.GetComponent<InputField>().text;
        string width = iWidth.GetComponent<InputField>().text;
        string height = iHeight.GetComponent<InputField>().text;

        if (string.IsNullOrEmpty(length))
            length = transform.localScale.x.ToString();
        if (string.IsNullOrEmpty(height))
            height = transform.localScale.z.ToString();
        if (string.IsNullOrEmpty(width))
            width = transform.localScale.y.ToString();


        targetSize = new Vector3(float.Parse(length), float.Parse(height), float.Parse(width));



        Mesh m = gameObject.GetComponent<MeshFilter>().sharedMesh;
        Bounds meshBounds = m.bounds;
        Vector3 meshSize = meshBounds.size;
        float xScale = targetSize.x / meshSize.x;
        float yScale = targetSize.y / meshSize.y;
        float zScale = targetSize.z / meshSize.z;
        transform.localScale = new Vector3(xScale, yScale, zScale);
    }

    void SetRootPosition()
    {
        float yPosition = root.transform.position.y;

        Vector3 vector3 = transform.position;

        vector3.y = yPosition;

        root.transform.position = vector3;
    }

    void Delete()
    {
        DestroyImmediate(root);

        foreach (Transform child in GameObject.FindGameObjectWithTag("ItemCanvas").transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        Destroy(gameObject);
    }
}
