using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	int[] guess = {0,0,0};
	int[] solution = {0,0,0};
	public float money;
	public int currentStreak;
	public int lastStreak;
	public int topStreak;

	public float updateTime;
	public bool minimumUpdateTime;
	public float sixtySecondsCounter;
	public float currentTime;
	public float initialTime;

	public GameObject black;
	public GameObject pinto;
	public Text moneyText;
	public Text currentStreakText;
	public Text topStreakText;
	public Text lastStreakText;
	public Text oddsText;
	public Text spmText;
	public Text WinLose;



	// Use this for initialization
	void Start () {
		hideBeans ();
		rollBeans ();
	}
	
	// Update is called once per frame
	void Update () {
		updateAllStats ();
	}

	public void setGuessHands(int leftOrRight){
		guess [1] = leftOrRight;
	}

	public void setGuessBeans(int blackOrPinto){
		guess [2] = blackOrPinto;
		//print ("guess hands:" + guess[1]);
		//print ("guess beans:" + guess[2]);
		checkGuess ();
		rollBeans ();
	}

	public bool checkGuess(){
		if((guess[1] == solution[1] && guess[2] == solution[2]) || (guess[1] != solution[1] && guess[2] != solution[2])){
			//print ("true");
			correct();
			return true;
		}
		//print("false");
		incorrect();
		return false;
	}

	private void rollBeans(){
		int leftOrRight = Random.Range(1,3);
		int blackOrPinto = Random.Range(1,3);

		solution [1] = leftOrRight;
		solution [2] = blackOrPinto;

		//print ("solution hand: " + leftOrRight);
		//print ("solution bean: " + blackOrPinto);

	}

	public float getMoney(){
		return money;
	}

	public void setMoney(float setMoney){
		money = setMoney;
	}

	public void addMoney(float addMoney){
		money += addMoney;
	}

	public void subtractMoney(float subMoney){
		money -= subMoney;
	}

	public int getStreak(){
		return currentStreak;
	}

	public int getLastStreak(){
		return lastStreak;
	}

	public int getTopStreak(){
		return topStreak;
	}

	public float getOdds(){
		float odds;
		if (currentStreak <= 0) {
			odds = 0;//-(Mathf.Pow(0.5f,(float)currentStreak));
		} else {
			odds = (Mathf.Pow(0.5f,(float)currentStreak));
			//print (odds);
		}
		return odds;
	}

	public int getSPM(){
		float newScorePerMinute = 123;
		//print (moneyValue.getMoney());

		if (minimumUpdateTime) {
			updateTime = Time.time;
		}

		if (sixtySecondsCounter < updateTime) {
			sixtySecondsCounter += Time.time;
			//print ("counting up" + sixtySecondsCounter);
			//float testSPM = (moneyValue.getMoney() - )/();
			//print ();
		} else {
			currentTime = Time.time;
			//money = moneyValue.getMoney ();

			newScorePerMinute = (money) / (currentTime);  //  $/m
			newScorePerMinute = newScorePerMinute * 60;  //  scorePerminute = scorePerMinute * 60s

			//print (currentTime);
			//print (newScorePerMinute + " = new spm");
			//print (currentMoney - initialMoney + " = delta money");
			//print (currentTime - initialTime + " = delta time");

			initialTime = Time.time;
			//money = moneyValue.getMoney ();  //its getting this every .5 seconds, i need a better way to tell the difference...
		}
		/*
		if (newScorePerMinute > scorePerMinute) {
			scorePerMinute = newScorePerMinute;
		}

		if (counter < updateTime) {
			counter += Time.deltaTime;
		} else {

			initialMoney = moneyValue.getMoney ();
			initialTime = Time.deltaTime;
			counter = 0;
		}
		*/
		return (int)newScorePerMinute;
	}

	public void showBeans(){
		black.SetActive(true);
		pinto.SetActive(true);
	}

	public void hideBeans(){
		black.SetActive (false);
		pinto.SetActive (false);
	}

	private void correct(){
		if (currentStreak <= 0) {
			//negative streak stuff
			currentStreak = 1;
		} else {
			currentStreak++;
		}
		if (currentStreak >= topStreak) {
			topStreak = currentStreak;
		}
		//WIN TEXT
		string winnerText = randomWinText();
		WinLose.text = winnerText;				//set correct message
		//ADD MONEY
		int winProfit = (int)Mathf.Pow (2, currentStreak);  //eventually 2 is replaced by (leftBeanProfit + rightBeanProfit)
		addMoney (winProfit);
		updateMoneyText ();
	}

	private void incorrect(){
		lastStreak = currentStreak;
		if (currentStreak >= 0) {
			currentStreak--;
		} else if (currentStreak < 0) {

		}
		WinLose.text = "Sorry! You Suck.";
		currentStreak = 0;
		int lossProfit = (int)Mathf.Pow(2,0);  //eventually 2 is replaced by (leftBeanProfit + rightBeanProfit)
		//print(lossProfit);
		addMoney (lossProfit);
		updateMoneyText();
	}

	private void updateMoneyText(){
		moneyText.text = "$" + money;
	}

	private void updateTopStreak(){
		topStreakText.text = topStreak.ToString();
	}

	private void updateCurrentStreak(){
		currentStreakText.text = currentStreak.ToString();
	}

	private void updateLastStreak(){
		lastStreakText.text = lastStreak.ToString();

	}

	private void updateOdds(){
		float multipliedOdds = (float)(getOdds ()*100);
		oddsText.text = multipliedOdds.ToString() + "%";
	}

	private void updateSPM(){
		spmText.text = getSPM() + "\n$/min";
	}

	private void updateAllStats(){
		updateSPM ();
		updateOdds ();
		updateTopStreak ();
		updateCurrentStreak ();
		updateLastStreak ();
		//save money
	}

	private string randomWinText() {
		string[] winTexts = { "Nice Job!", "Correct!", "You're on a streak!" };
		return winTexts [Random.Range (0, winTexts.Length+1)];
	}

	private string randomLoseText() {
		string[] loseTexts = { "Nice Job!", "Correct!", "You're on a streak!" };
		return loseTexts [Random.Range (0, loseTexts.Length+1)];
	}

}
