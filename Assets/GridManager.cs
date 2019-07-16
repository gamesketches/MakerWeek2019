using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GridManager : MonoBehaviour
{

	public GameObject cell;
	public int width;
	public int height;


    // Start is called before the first frame update
    void Start()
    {
	    string path = "assets/Resources/gender_and_jobs_data.csv";

	    StreamReader reader = new StreamReader(path);
		int numRows = 0; 
		while(reader.Peek() >= 0) {
			string newData = reader.ReadLine();
			string[] data = newData.Split(new char[] {','});
			GameObject temp = Instantiate(cell);
			temp.transform.position = new Vector3(2, 2 * numRows, 0);
			temp.GetComponent<SquareBehavior>().ChangeColor(float.Parse(data[1]));
			temp = Instantiate(cell);
			temp.transform.position = new Vector3(4, 2 * numRows, 0);
			temp.GetComponent<SquareBehavior>().ChangeColor(float.Parse(data[2]));
			numRows++;
		}
        /*for(int i = 0; i < width; i++){
			for(int j = 0; j < height; j++) {
				GameObject temp = Instantiate(cell); 
				temp.transform.position = new Vector3(2 * i, 2 * j, 0);
			}
		}*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
