/*
 * TouchScroll - using dom overflow:scroll
 * by kmturley
 * https://github.com/kmturley/touch-scroll/blob/master/index.html
 */

/*globals window, document */

export class TouchScroll {

  el: HTMLElement;

  private drag = false;

  private diffx: number = 0;
  private diffy: number = 0;
  private startx: number = 0;
  private starty: number = 0;
  private lastAnimationFrame: number;

  /**
   * @method init
   */
  constructor(options: { el: HTMLElement }) {
    // find target element or fall back to body
    if (options && options.el) {
      this.el = options.el;
    }
    else {
      this.el = document.body;
    }

    this.el.addEventListener('mousedown', this.onMouseDown);
    this.el.addEventListener('mousemove', this.onMouseMove);
    document.body.addEventListener('mouseup', this.onMouseUp);
  }

  destroy() {
    this.el.removeEventListener('mousedown', this.onMouseDown);
    this.el.removeEventListener('mousemove', this.onMouseMove);
    document.body.removeEventListener('mouseup', this.onMouseUp);
    this.el.classList.add('touch-scroll-grabbing');
  }

  /**
   * @method cancelEvent
   */
  private cancelEvent(e) {
    if (!e) { e = window.event; }
    if (e.target && e.target.nodeName === 'IMG') {
      e.preventDefault();
    } else if (e.srcElement && e.srcElement.nodeName === 'IMG') {
      e.returnValue = false;
    }
  }

  /**
   * @method onMouseDown
   */
  private onMouseDown = (e) => {
    if (this.drag === false) {
      this.drag = true;
      this.cancelEvent(e);
      this.startx = e.clientX + this.el.scrollLeft;
      this.starty = e.clientY + this.el.scrollTop;
      this.diffx = 0;
      this.diffy = 0;
      this.el.classList.add('touch-scroll-grabbing');
    }
  }

  /**
   * @method onMouseMove
   */
  private onMouseMove = (e) => {
    if (this.drag === true) {
      this.cancelEvent(e);
      this.diffx = (this.startx - (e.clientX + this.el.scrollLeft));
      this.diffy = (this.starty - (e.clientY + this.el.scrollTop));

      if (this.lastAnimationFrame)
        window.cancelAnimationFrame(this.lastAnimationFrame);

      this.lastAnimationFrame = window.requestAnimationFrame(() => {
        this.el.scrollLeft += this.diffx;
        this.el.scrollTop += this.diffy;
        this.lastAnimationFrame = 0;
      });
    }
  }

  /**
   * @method onMouseMove
   */
  private onMouseUp = (e) => {
    if (this.drag === true) {
      this.cancelEvent(e);
      this.drag = false;
      this.el.classList.remove('touch-scroll-grabbing');
    }
  }
}
