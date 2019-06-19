import { EventEmitter } from "@stencil/core";
import Api from "./api.typings";

// From: https://github.com/ionic-team/ionic/blob/master/core/src/utils/helpers.ts

export function deferEvent(event: EventEmitter): EventEmitter {
  return debounceEvent(event, 0);
}

export function debounceEvent(event: EventEmitter, wait: number): EventEmitter {
  const original = (event as any)._original || event;
  return {
    _original: event,
    emit: debounce(original.emit.bind(original), wait) as any
  } as EventEmitter;
}

export function debounce(func: (...args: any[]) => void, wait = 0) {
  let timer: any;
  return (...args: any[]): void => {
    clearTimeout(timer);
    timer = setTimeout(func, wait, ...args);
  };
}

export function removeAccents(str:string) {
  return str.normalize('NFD').replace(/[\u0300-\u036f]/g, '');
}

export function join(str: any | any[], separator: string = ', ', lastSeparator?: string): string {
  if (!str || !str.length)
    return '';

  if (typeof str === 'string')
    return str;

  if (Array.isArray(str) && str.length == 1)
    return str[0];

  if (lastSeparator === undefined)
    lastSeparator = separator;

  let terms = str
    .filter(p => p !== null && p !== undefined && (!p.trim || !!p.trim()))
    .reduce((a, b) => Array.isArray(b) ? [...a, ...b] : a.push(b) && a, []);

  if (separator !== lastSeparator)
    return terms.slice(0, terms.length - 1).join(separator) + lastSeparator + terms[terms.length - 1];

  return terms.join(separator);
}

export function formatDate(date: string | number | Date, format: 'short' | 'long' = 'short') {
  if (typeof date === 'string')
    date = new Date(date);

  if (format == 'short')
    return new Intl.DateTimeFormat('pt-BR').format(date).replace('/', '.').replace('/', '.');

  return new Intl.DateTimeFormat('pt-BR', { day: '2-digit', month: 'long', year: 'numeric' }).format(date);
}

export function obterPerfilDescricao(perfil: Api.Server.Domain.FamiliaUsuarioPerfilEnum) {
  switch (perfil) {
    case Api.Server.Domain.FamiliaUsuarioPerfilEnum.Financeiro:
      return 'Financeiro';

    case Api.Server.Domain.FamiliaUsuarioPerfilEnum.Contratante:
      return 'Contratante';

    case Api.Server.Domain.FamiliaUsuarioPerfilEnum.SuporteBrasil:
      return 'Suporte Brasil';

    default:
      return '';
  }
}
