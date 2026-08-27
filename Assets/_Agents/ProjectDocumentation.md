# **[GAME TITLE]**
*Game Design Document*

**Genre:** Survival Horror[cite: 1]  
**Platform:** PC[cite: 1]  
**Version:** 1.1  
**Date:** 26/August/2026[cite: 1]  
**Author(s):** Ahmed Bahaa / Jana Sherif / Joy Magdy[cite: 1]  
**Team Name:** Khosary Code[cite: 1]  

---

## **1. Game Overview**

### **1.1 Game Concept and Direction**
The game takes place in a standard, terrifying hospital setting. The player controls a mad patient seeking revenge on the hospital doctors for injecting them with syringes[cite: 1]. 
*   **Genre:** Survival Horror[cite: 1]. 
*   **Movement Reference:** The Escapists (Top-down 2D perspective)[cite: 1].

### **1.2 The Intended Player Experience**
The player will feel a frantic sense of urgency. The primary goal is to knock out the doctors before a strict timer runs out[cite: 1]. If the timer reaches zero, the patient passes out and is thrown into a coma[cite: 1].

### **1.3 Design Pillars**
*   **Knockout City:** Knock out the doctors before they knock you out[cite: 1].
*   **Race Against Time:** Knock them out before the timer ends[cite: 1]. The timer is absolute and cannot be extended or altered by any mechanics.
*   **Vulnerability & Attrition:** Try not to be caught[cite: 1]. There is **NO HEALING** in the game; health can only go down.
*   **Adrenaline Management:** Balancing speed and survival through dynamic adrenaline levels.

### **1.4 Game Loops**
The game begins as doctors attempt to anesthetize the patient, but the patient escapes with a syringe[cite: 1]. A strict countdown timer begins. The player must navigate the hospital, using stealth and speed to knock out doctors and avoid security[cite: 1]. If the timer ends, or health reaches zero, the doctors take control again[cite: 1].

---

## **2. Gameplay Mechanics**

### **2.1 Core Mechanics**
*   **Strict Timer:** A countdown timer that leads to a game over when it hits zero[cite: 1]. It ticks down constantly and is unaffected by any in-game actions.
*   **Health System (No Healing):** The player has a set amount of health that decreases upon taking damage from security or using specific abilities[cite: 1]. Health can never be regenerated.
*   **Adrenaline Bar:** A dynamic meter that dictates player movement speed. As adrenaline increases, the player's movement speed increases. As it decreases, the player slows down, making them vulnerable.
*   **Syringe Dash (Player Attack):** The player wields a syringe[cite: 1]. Pressing attack initiates a high-risk dash. Connecting with a doctor results in an instant knockout. Missing the dash leaves the player temporarily slowed. 

### **2.2 Secondary Mechanics (Pickups & Traps)**
*   **Electric Wire (Environmental Pickup):** Wires found in the environment can be picked up and used tactically[cite: 1].
    *   **Adrenaline Overcharge:** The player can shock themselves with the wire to trigger an Overcharge[cite: 1]. **Cost:** Reduces the player's health. **Reward:** Maximizes the Adrenaline Bar (massive speed boost) and grants total invulnerability for a short, set duration.
    *   **Doorway Traps:** Wires can be strung across doorframes to stun pursuing security guards.
*   **Doctor & Security AI:** Doctors flee or panic, while Security Guards actively chase and attack using projectile guns[cite: 1]. 

### **2.3 Controls & Input Scheme**
*   **WASD:** Movement[cite: 1].
*   **Space:** Syringe Dash / Attack[cite: 1].
*   **E:** Interact (Pick up items, set wire traps, trigger Overcharge)[cite: 1].

---

## **3. Game Systems**

### **3.1 Gameplay Systems Specifications**
*   **AI State Machines:** Enemy behaviors (Doctors and Security) are driven by C# Finite State Machines, allowing them to smoothly transition between Patrol, Panic, Flee, Chase, and Knocked-Out states.
*   **Pathfinding:** Enemies utilize A* navigation to pursue the player around hospital beds, corridors, and equipment.

---

## **4. Game Context (Story)**

### **4.1 Setting / World Premise**
A cold, sterile hospital at night. The doctor attempts to inject the patient, but the patient steals the syringe and begins hunting the staff for revenge[cite: 1].

### **4.2 Theme & Tone**
*   Horror[cite: 1].
*   Tense, high-speed action mixed with stealth.

---

## **6. UI/UX Design**

### **6.1 HUD Design (in-game overlays)**
*   **Timer:** Prominent countdown[cite: 1].
*   **Health Bar:** Slowly depletes, never refills[cite: 1].
*   **Adrenaline Bar:** Visual indicator of current speed tier.

### **6.2 Menu Systems**
*   Main Menu[cite: 1].
*   Opening Cutscene (triggers upon pressing Play)[cite: 1].
*   Gameplay Scene[cite: 1].
*   Pause UI & Game Over UI[cite: 1].

### **6.3 UI Feedback Systems**
*   Electric shock particle effect when using the wire[cite: 1].
*   Camera shake on impact or taking damage[cite: 1].
*   Red vignette indicating low health or damage taken[cite: 1].

---

## **7. Art & Audio Direction**

### **7.1 Visual Style**
*   Hospital setting with night vibes and horror elements[cite: 1].
*   The Escapists style (Top-down)[cite: 1].
*   Detailed 2D Pixel Art lit by high-contrast URP 2D lighting to cast dynamic shadows in the dark hospital wings. 

### **7.2 Character & Environment Art**
*   2D Tilemap for the hospital environment (walls, floors, medical equipment)[cite: 1].
*   Custom 2D pixel character sprites utilizing sprite sheets for multi-directional movement.

### **7.3 Animation Required**
*   Idle & Running[cite: 1].
*   Knocking out by syringe (custom takedown animation)[cite: 1].
*   Death/Passing out[cite: 1].
*   Electric wire interaction and self-shock animation[cite: 1].
*   Doors opening/closing[cite: 1].