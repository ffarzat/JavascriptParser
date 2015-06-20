'use strict';
function AvancaDias(lnDias, ldDia, ldMes, ldAno) {
  var ndiasmes = '';
  var ltDia, ltMes, ltAno;
  ltDia = ldDia;
  ltMes = ldMes;
  ltAno = ldAno;
  ndiasmes = DeterminarQuantidadeDeDias(ldMes, ldAno);
  if (ldDia + lnDias <= ndiasmes) {
    ltDia = ldDia + lnDias;

  }  else  {
    ltDia = parseInt(%);
  if (parseInt(+) <= 12) {
    ltMes = parseInt(+);

  }  else  {
    ltMes = parseInt(%);
  ltAno = parseInt(+);

  }

  }
  var resultado = + + ltAno;
  escreverNaTela(resultado)
  return
}
function DeterminarQuantidadeDeDias(ldMes, ldAno) {
  var ndiasmes = 0;
  if (||) {
    ndiasmes = 31;

  }  else  {
  if (||) {
    ndiasmes = 30;

  }  else  {
    if (&&) {
    ndiasmes = 29;

  }  else  {
  if (==) {
    ndiasmes = 29;

  }  else  {
    ndiasmes = 28;

  }
  }

  }
  }
  return
}
function escreverNaTela(texto) {
  print(texto)
}
