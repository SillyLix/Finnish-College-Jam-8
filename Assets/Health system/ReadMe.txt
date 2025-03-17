Simple Health System

This contains a basic implementation of a health system designed for use in Unity games. The system includes components for managing health, dealing damage, and  displaying health UI elements to players.

Setup Instructions

1. Assigning Components:
   - Attach the HealthSystem component to any game object that needs health management.
   - Add the DamageSystem component to objects capable of dealing damage, adjusting damage values as needed through the Inspector.

2. Setting Up Health UI:
   - Add the HeathBar prefab to the canva.
   - Add the HealthUI component to the same object as your HealthSystem.
   - Drag and drop your preconfigured HealthBar prefab into the specified field within the HealthUI component's settings in the Inspector.

Usage

- Use the TakeDamage method of the DamageSystem component to apply damage to objects with a HealthSystem.
This will decrease their health, triggering appropriate events or actions based on your implementation.
- Monitor the health status visually through the HealthUI, which updates dynamically as health values change.

Requirements

- Unity Engine 6000+ (tested with version 6000.0.35f1)
- Basic understanding of Unity scripting and components