import { Component, ElementRef } from '@angular/core';

@Component({
  selector: 'app-modal-base',
  templateUrl: './modal-base.component.html',
  styleUrls: ['./modal-base.component.css']
})
export class ModalBaseComponent {

  constructor(private el: ElementRef){

  }

  ngOnInit(){
    this.el.nativeElement.addEventListener('click',()=>{
      this.close();
    })
  }

  close(){
    this.el.nativeElement.classList.remove('sshow');
    this.el.nativeElement.classList.remove('hhidden');
  }
}
