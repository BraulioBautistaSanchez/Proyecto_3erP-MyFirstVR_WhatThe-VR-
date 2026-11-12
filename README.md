# What The VR? — Golf, Boliche y Gravedad

Experiencia de realidad virtual / mixta para **Meta Quest**, hecha en Unity e inspirada en el estilo experimental de *What The Golf?*. El jugador explora un mundo de **islas flotantes** donde combina golf, boliche y distintas gravedades, usando una resortera con las manos y un palo de golf agarrable.

> **Autor:** Braulio Bautista Sánchez

> **Materia:** Simulación Física para Realidad Virtual y Videojuegos

> **Docente:** Pamela Estefanía Chávez López

> **Video de demostración:** https://youtu.be/B8ZgqgmEcQs?si=KC4h_KVEkGe-IiSG 


## Descripción

Una sola escena interactiva con tres islas flotantes animadas, cada una con su propia dinámica:

| Isla | Dinámica |
|---|---|
| **Isla de golf** | Hoyo con bandera. Se mete la pelota con el palo o con la resortera para sumar puntos. |
| **Isla de boliche** | Pinos con física real. Se derriban con una pelota pesada y cada pino caído suma 1 punto. |
| **Isla de objetos** | Aqui se encuentran los objetos a intercatuar y el respawn del player. |

---

## Requisitos

- **Unity 6000.3.24f1** (Unity 6.3 LTS), con el módulo de **Android Build Support**.
- **Meta XR SDK** (incluido en el proyecto) y Building Blocks.
- **Git** y **Git LFS** instalados.
- Uno de estos para ejecutarlo:
  - Meta Quest con **Quest Link / Air Link** (recomendado), o
  - **Meta XR Simulator**, o
  - un APK instalado en el visor.

---

## Cómo abrir el proyecto

1. Instala Git LFS **antes** de clonar (los modelos, texturas y audio se guardan con LFS):
   ```bash
   git lfs install
   git clone https://github.com/BraulioBautistaSanchez/Proyecto_3erP-MyFirstVR_WhatThe-VR-.git
   ```
2. Abre **Unity Hub > Add > Add project from disk** y selecciona la carpeta clonada.
3. Usa la versión de Unity **6000.3.24f1**. La primera apertura tarda porque Unity regenera la carpeta `Library`.
4. Abre la escena principal: `Assets/MyFirstVR/Scenes/` _(indicar el nombre de la escena)_.

Si los modelos o texturas aparecen rosas o rotos, ejecuta `git lfs pull` dentro de la carpeta del proyecto.

---

## Cómo ejecutarlo

**Con Quest Link (PC):**
1. Conecta el visor y activa Quest Link.
2. En Unity, abre la escena y pulsa **Play**.
3. Espera unos 2 segundos a que el visor empiece a rastrear antes de moverte.

**Con Meta XR Simulator:** activa el simulador en *Meta > Meta XR Simulator* y pulsa **Play**. *(Por revisar)*

**En el visor (APK):** *File > Build Profiles > Android*, selecciona el Quest conectado y pulsa **Build And Run**. *(Por revisar)*

---

## Controles y mecánicas principales

### Resortera
- **Pellizca con ambas manos** (o gatillo del control) para tensar la resortera.
- La **dirección** y la **fuerza** dependen de la distancia y del ángulo al estirar; al soltar, la pelota sale disparada.
- La fuerza se ajusta con `Force Multiplier` en el prefab `Logic`.
- Script: `SlingshotMechanic`.

### Palo de golf
- Agarrable con la mano. La cabeza del palo mide su velocidad y la transmite a la pelota al golpear.
- Script: `ClubHead`.

### Teleport (en proceso)
- Con la **mano izquierda** apunta y pellizca para teletransportarte.
- Solo funciona sobre las **islas**; fuera de ellas no hay destino válido.
- Hecho con Meta Building Blocks (Interaction SDK).

### Caída y respawn (incompleto)
- Si te sales de una isla, caes. Al pasar cierta altura, regresas al punto de aparición.
- Las pelotas que caen al vacío vuelven a su posición inicial (o se destruyen si fueron lanzadas con la resortera).
- Scripts: `PlayerRespawn`, `FloorRespawnZone`.

### Gravedades distintas (por realizar)
Zonas que modifican la gravedad (jugando con las fisicas de las pelotas por ejemplo):

| Pelota | Gravedad | Comportamiento |
|---|---|---|
| _(pelota 1)_ | 1x | Normal |
| _(pelota 2)_ | _(valor)_ | _(ej. flota como en la Luna)_ |
| _(pelota 3)_ | _(valor)_ | _(ej. pesada, cae rápido)_ |

- Scripts: `CustomGravity`, `GravityZone`.
- Se usan `Rigidbody` y `Collider` en todos los objetos con física.

### Puntaje
- **Hoyo de golf:** 5 puntos.
- **Pino derribado:** 1 punto cada uno.
- El marcador aparece frente al jugador (HUD en World Space) y salen textos flotantes animados con cada punto.
- Scripts: `ScoreManager`, `GoalDetector`, `PinScore`.

---

## Meta Building Blocks utilizados

- **Camera Rig**
- **Passthrough**
- **Teleport** (Interaction SDK)
- **Hand Tracking** (`OVRHand`) para la resortera y el agarre de objetos

---

## Estructura del proyecto

```
Assets/
└── MyFirstVR/
    ├── Animations/   # Animación de flotación de las islas
    ├── Art/          # Modelos 3D, materiales, skybox, texturas
    ├── Colliders/
    ├── Prefabs/      # Pelotas, pinos, palo, hoyo, textos flotantes, Logic
    ├── Scenes/
    └── Scripts/      # SlingshotMechanic, GoalDetector, ScoreManager, ...
```

---

## Notas técnicas

- El repositorio usa **Git LFS** para `.fbx`, `.png`, `.exr`, `.wav`, `.mp3` y otros archivos pesados.
- El skybox `lakeside_sunrise_4k.exr` pesa unos 92 MB. Para el visor se recomienda reducir su **Max Size** o usar una versión de menor resolución.
- Mantén el visor en un espacio libre al probar la caída y el respawn.

---

## Créditos

- Modelos propios: islas, pino, palo de golf y pelotas.
- Package de la mecánica de resortera: Por la docenet:.
- Inspiración: *What The Golf?*.
