# Automated Industrial Airlock — Blackbox Hardware-in-the-Loop (HIL) Validator

## System Overview & Objectives

The Automated Industrial Airlock Blackbox HIL Validator is a dedicated hardware-in-the-loop test bench designed for real-time functional, state-boundary, and safety verification of critical airlock controls.

The validator directly interfaces with physical actuators, solenoids, state feedback registers, and an AT28C64B EEPROM lookup matrix. By driving external address lines and sampling output responses in parallel with physical registers, the suite guarantees hazard-free state transitions, verified timing parameters, and complete isolation between hazardous cleanroom and exterior zones.

### Core Objectives

- Verify state integrity across dynamic operating conditions.
- Ensure physical locking solenoids retain safety locks during loss-of-signal or power transients.
- Eliminate race conditions across asynchronous input lines using atomic bus synchronization.
- Monitor real-time feedback loops for decontamination spray, pressure equalization valves, and access interlocks.

---

## Hardware Pin Mappings (Raspberry Pi T-Cobbler to EEPROM & Controls)

The following mapping defines the connection routing between the Raspberry Pi T-Cobbler interface, the AT28C64B EEPROM address/data buses, and the airlock state feedback control unit.

| Bus Category | Signal Name | Target Pin / Register | Raspberry Pi BCM | Physical Pin |
| --- | --- | --- | --- | --- |
| Input Control | Clock | EEPROM A0 | BCM 04 | Pin 7 |
| Input Control | SOS / Emergency | EEPROM A1 | BCM 05 | Pin 29 |
| Input Control | Service / Maintenance | EEPROM A2 | BCM 06 | Pin 31 |
| Input Control | Door B Signal | EEPROM A3 | BCM 13 | Pin 33 |
| Input Control | Door A Signal | EEPROM A4 | BCM 16 | Pin 36 |
| State Feedback | State Feedback Bit 0 | EEPROM A5 | Output Register Bit 0 | N/A (Register) |
| State Feedback | State Feedback Bit 1 | EEPROM A6 | Output Register Bit 1 | N/A (Register) |
| State Feedback | State Feedback Bit 2 | EEPROM A7 | Output Register Bit 2 | N/A (Register) |
| State Feedback | State Feedback Bit 3 | EEPROM A8 | Output Register Bit 3 | N/A (Register) |
| Output Observation | Pressure Vent | EEPROM I/O4 | BCM 17 | Pin 11 |
| Output Observation | Decontamination Spray | EEPROM I/O5 | BCM 18 | Pin 12 |
| Output Observation | Lock B Control | EEPROM I/O6 | BCM 19 | Pin 35 |
| Output Observation | Lock A Control | EEPROM I/O7 | BCM 20 | Pin 38 |

---

## Transition Strategy: Applying Atomic Bus Synchronization

In high-reliability hardware validation setups, toggling GPIO pins sequentially introduces microsecond-level intermediate skew states. These transient intermediate states can trigger unintended logic lookups in parallel EEPROMs or state registers, leading to spurious solenoid activations or false safety trips.

To eliminate race conditions, the validation engine enforces an atomic synchronization pattern via the `SetInputs` method.

### Key Synchronization Mechanics

1. **Shadow Register Masking:** All input state changes (Clock, SOS, Service, Door Signals) are prepared in a local memory buffer as a single combined byte mask.
2. **Parallel Port Writes:** The output mask is written to the physical GPIO bus in a single clock cycle operation (or via direct register block write memory mapping `/dev/gpiomem`), preventing intermediate transient values.
3. **Settling Time & Latch Verification:** A deterministic hold time (`tSETTLE` ≥ 150 ns) is observed before reading data back from the EEPROM I/O lines to account for propagation delays across physical line capacitance.
4. **Readback Validation:** Output observation lines (BCM 17–20) are sampled synchronously after the settling window to verify that the hardware state matches expected lookup tables.

