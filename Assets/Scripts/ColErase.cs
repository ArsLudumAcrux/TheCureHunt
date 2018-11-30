using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColErase : MonoBehaviour {



	public int ValorMoeda;
	public int ValorExp;




	// Caso a moeda se chocar com o jogador, ela será destruída e o ouro será somado.
		void OnTriggerEnter2D(Collider2D col) {
		if (col.CompareTag ("Attack")) {
			Destroy (gameObject);
		}
		else if (col.CompareTag ("Player")){
			Destroy (gameObject);

		}
			
	}

/*		
		----BLOCO DELETADO----
		
	      O bloco abaixo resumidamente checaria a cada frame se já existe algum tempo na variável DestructionTime para iniciar a 
	   contagem de tempo para assim poder destruir o objeto.
	   
	      O que ele vai fazer basicamente é sempre adicionar ao valor da variável DestructionTime o resultado da soma entre o valor
	   anterior da mesma variável com o valor de Time.deltaTime, ou seja, quando o valor de DestructionTime for definido como 1 no
	   bloco acima, o bloco abaixo vai pegar o valor dele (1) e fará a conta:  1 mais 1 vezes o número de segundos que já passou
	   desde o início do jogo.

	void Update() {
		
		DestructionTime = DestructionTime + (DestructionTime * Time.deltaTime);
		if (DestructionTime >= 1.5f) {
			Destroy (gameObject);
		}
	}
	
*/

}