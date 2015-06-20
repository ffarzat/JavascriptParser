'use strict'
function AvancaDias(lnDias, ldDia, ldMes, ldAno) {
  var ndiasmes = ''
  var ltDia, ltMes, ltAno
  ltDia = ldDia
  ltMes = ldMes
  ltAno = ldAno
  ndiasmes = DeterminarQuantidadeDeDias(ldMes, ldAno)
  if (ldDia + lnDias <= ndiasmes) {
    ltDia = ldDia + lnDias;

  }  else  {
    ltDia = parseInt((ldDia + lnDias) % ndiasmes);
  if (parseInt(ldMes + (ldDia + lnDias) / ndiasmes) <= 12) {
    ltMes = parseInt(ldMes + (ldDia + lnDias) / ndiasmes);

  }  else  {
    ltMes = parseInt((ldMes + (ldDia + lnDias) / ndiasmes) % 12);
  ltAno = parseInt(ldAno + (ldMes + (ldDia + lnDias) / ndiasmes) / 12);

  };

  }
  var resultado = ltDia + '/' + ltMes + '/' + ltAno
  escreverNaTela(resultado)
  return resultado
}
function DeterminarQuantidadeDeDias(ldMes, ldAno) {
  var ndiasmes = 0
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
  };

  }
  }
  return ndiasmes
}
function escreverNaTela(texto) {
  print(texto)
}
