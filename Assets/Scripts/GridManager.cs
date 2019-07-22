using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class GridManager : MonoBehaviour
{

	public GameObject cell;
	public GameObject yearPrefab;
	public int width;
	public int height;
	List<GameObject[]> blocks;
	List<GameObject> yearColumn;


    // Start is called before the first frame update
    void Start()
    {
	    string path = "assets/Resources/gender_and_jobs_data.csv";

		blocks = new List<GameObject[]>();
		yearColumn = new List<GameObject>();
	    StreamReader reader = new StreamReader(path);
		int numRows = 0; 
		GameObject temp;
		while(reader.Peek() >= 0) {
			string newData = reader.ReadLine();
			string[] data = newData.Split(new char[] {','});
			GameObject[] tempRow = new GameObject[data.Length];
			for(int i = 0; i < data.Length; i++) {
				temp = Instantiate(cell);
				temp.transform.position = new Vector3(-3 + 4.5f * i, 3 * numRows, 0);
				temp.GetComponent<SquareBehavior>().ChangeColor(float.Parse(data[i]));
				temp.GetComponentInChildren<TextMeshPro>().text = data[i];
				tempRow[i] = temp;
			}
			blocks.Add(tempRow);
			numRows++;
		}
		for(int j = 0; j < numRows; j++) {
        	temp = Instantiate(yearPrefab);
			temp.transform.position = new Vector3(-9, 3 * j, 0);
			temp.GetComponent<TextMeshPro>().text = (1962 +j).ToString();
			yearColumn.Add(temp);
		}
    }

    // Update is called once per frame
    void Update()
    {
		bool stillThere = false;
		GameObject[] curRow = blocks[0];
		for(int i = 0; i < curRow.Length; i++){
			if(curRow[i] != null) {
				stillThere = true;
				break;
			}
		}
		if(!stillThere) {
			blocks.RemoveAt(0);
			MoveDownBlocks();
		}
    }

	void MoveDownBlocks() {
		for(int i = 0; i < blocks.Count; i++) {
			foreach(GameObject block in blocks[i]) {
				StartCoroutine(ShiftDown(block, 2f,0.7f));
			}
		}
		Destroy(yearColumn[0]);
		yearColumn.RemoveAt(0);
		for(int i = 0; i < yearColumn.Count; i++) {
			StartCoroutine(ShiftDown(yearColumn[i], 2f, 0.7f));
		}
	}

	IEnumerator ShiftDown(GameObject theBlock, float shiftAmount, float totalTime) {
		Vector3 startPosition = theBlock.transform.position;
		Vector3 endPosition = new Vector3(startPosition.x, startPosition.y - shiftAmount, startPosition.z);
		for(float t = 0; t < totalTime; t += Time.deltaTime) {
			theBlock.transform.position = Vector3.Lerp(startPosition, endPosition, t / totalTime);
			yield return null;
		}
		theBlock.transform.position = endPosition;
	}
	
}
