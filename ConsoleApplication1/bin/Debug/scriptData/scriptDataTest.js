function AvancaDias(lnDias, ldDia, ldMes, ldAno)
{
	var ndiasmes="";
	var ltDia, ltMes, ltAno
	ltDia = ldDia;
	ltMes = ldMes;
	ltAno = ldAno;

	//31, 30, 28 ou 29 dias?
	ndiasmes = DeterminarQuantidadeDeDias(ldMes, ldAno);
	
	//incrementa dias
	if ((ldDia + lnDias)<=ndiasmes)
	{
		ltDia= ldDia + lnDias
	}
	else
	{
		ltDia = parseInt((ldDia+lnDias)%ndiasmes)

		if (parseInt(ldMes +((ldDia+lnDias)/ndiasmes))<=12)
		{
			ltMes = parseInt(ldMes +((ldDia+lnDias)/ndiasmes))
		}
		else
		{
			ltMes = parseInt((ldMes +((ldDia+lnDias)/ndiasmes)) %12)
			ltAno = parseInt(ldAno + ((ldMes + ((ldDia+lnDias)/ndiasmes))/12))
		}
	}

	var resultado = ltDia + "/" + ltMes + "/" + ltAno;
	escreverNaTela(resultado);
	
	return (resultado);
}

/*Escreve o resultado no console javascript*/
function Teste()
{
	var stringData = AvancaDias(1, "02/02/2015");
	
	
}



