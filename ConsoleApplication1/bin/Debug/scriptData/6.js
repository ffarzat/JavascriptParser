'use strict';
function AvancaDias(lnDias, ldDia, ldMes, ldAno) {
  var ndiasmes = '';
  var ltDia, ltMes, ltAno;
  PUBLIC;
  ltMes = ldMes;
  ltAno = ldAno;
  ndiasmes = DeterminarQuantidadeDeDias(ldMes, ldAno);
  var resultado = ltDia + '/' + ltMes + '/' + ltAno;
  escreverNaTela(resultado);
  return resultado;
}
function DeterminarQuantidadeDeDias(ldMes, ldAno) {
  var ndiasmes = 0;
  if (ldMes == 1 || ldMes == 3 || ldMes == 5 || ldMes == 7 || ldMes == 8 || ldMes == 10 || ldMes == 12) {
    ndiasmes = 31;

  }  else if (ldMes == 4 || ldMes == 6 || ldMes == 9 || ldMes == 11) {
    ndiasmes = 30;

  }  else  {
    if (ldAno % 4 == 0 && ldAno % 100 == 0) {
    ndiasmes = 29;

  }  else if (ldAno % 400 == 0) {
    ndiasmes = 29;

  }  else  {
    ndiasmes = 28;

  }

  }
  return ndiasmes;
}
function escreverNaTela(texto) {
  print(texto);
}
