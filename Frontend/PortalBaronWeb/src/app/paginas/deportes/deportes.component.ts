import { Component } from '@angular/core';
import { HeaderComponent } from '../../componentes/header/header.component';
import { AddComponent } from '../../componentes/add/add.component';

@Component({
  selector: 'app-deportes',
  imports: [HeaderComponent, AddComponent],
  templateUrl: './deportes.component.html',
  styleUrl: './deportes.component.css'
})
export class DeportesComponent {
    // Datos del equipo (puedes mover esto a un servicio)
    virtusImage = '../assets/images/virtus.jpg'; // Ruta relativa al componente
}
