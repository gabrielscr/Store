export class EventTarget {
  private listeners = { };

  addEventListener(type: string, callback: (event: Event) => any) {
    if (!(type in this.listeners)) {
      this.listeners[type] = [];
    }

    this.listeners[type].push(callback);
  }

  removeEventListener(type: string, callback: (event: Event) => any) {
    if (!(type in this.listeners)) {
      return;
    }

    let stack = this.listeners[type];
    for (let i = 0, l = stack.length; i < l; i++) {
      if (stack[i] === callback) {
        stack.splice(i, 1);
        return;
      }
    }
  }

  protected dispatchEvent(event: Event) {
    if (!(event.type in this.listeners)) {
      return true;
    }

    let stack = this.listeners[event.type];

    for (let listener of stack) {
      listener.call(this, event);
    }

    return !event.defaultPrevented;
  }
}
