# Automated Industrial Airlock — Offline FSM Verification Harness

This document outlines the architecture, hardware interface definitions, and execution model for the Raspberry Pi-driven offline verification harness. The system is engineered to rigorously validate the AT28C64B EEPROM lookup table controller for an automated industrial airlock system without relying on live external target hardware.

## System Overview & Objectives

The primary purpose of this test harness is to perform deterministic validation of state machine transition vectors programmed into an AT28C64B parallel EEPROM chip. By simulating system inputs via bit-banged GPIO operations and reading back output states directly from the ROM, the harness isolates and verifies finite state machine (FSM) behavior in a controlled environment.

* **Target Hardware:** AT28C64B 64K (8K x 8) Parallel EEPROM.
* **Control Platform:** Raspberry Pi Zero W (ARMv6 architecture).
* **Execution Environment:** Mono Runtime for .NET framework execution on Linux.
* **Primary Objective:** Execute complete functional path coverage across all encoded lock logic, pressure equalizing sequences, and safety failure modes without driving physical actuators or high-voltage relays.

## Key Engineering Breakthroughs

### 1. Atomic Bus Synchronization

Conventional bit-banging implementations suffer from propagation skew when asserting multi-bit address lines sequentially, potentially causing spurious memory reads or transient intermediate states. This harness resolves bus skew by bundling the primary clock/latch pulse (A0) directly with the target address vector into single, unified atomic writes via native low-level memory mappings (`SetInputs`). This guarantees that address setup times (tAS) are fully satisfied prior to output evaluation without intermediate illegal states.

### 2. Static Hardware Initialization

To eliminate setup latency and unstable pin states during test suite startup, the harness implements a static 17-pin GPIO configuration layer. By maintaining a persistent initialization context across test fixtures, the system bypasses the high file-descriptor and register reset overhead inherent to legacy `WiringPi` and standard `sysfs` re-initialization calls. This ensures clean, microsecond-accurate input assertions and stable high-impedance pull setups throughout long-running test suites.

### 3. Silicon Integrity & Multimeter Validation

To guarantee physical fidelity between software assertions and actual silicon states, the suite incorporates hardware-level signal verification protocols. Static hardware diagnostic modes allow manual multimeter probing across all 17 signal lines. Furthermore, automated full-range memory dumps verify physical array integrity against expected binary checksums before executing functional FSM path tests, preventing physical IC degradation from yielding false software failures.

## Hardware Pin Mappings (Raspberry Pi to AT28C64B)

The table below maps the physical and logical GPIO routing from the Raspberry Pi pin header to the AT28C64B EEPROM DIP pins.

| Signal Function | EEPROM Signal | Pi BCM Pin | Pi Physical Pin | Signal Direction |
| --- | --- | --- | --- | --- |
| Clock / Latch | A0 | BCM 04 | Pin 7 | Output (Pi -> EEPROM) |
| Address Bus | A1 | BCM 05 | Pin 29 | Output (Pi -> EEPROM) |
| Address Bus | A2 | BCM 06 | Pin 31 | Output (Pi -> EEPROM) |
| Address Bus | A3 | BCM 13 | Pin 33 | Output (Pi -> EEPROM) |
| Address Bus | A4 | BCM 16 | Pin 36 | Output (Pi -> EEPROM) |
| Address Bus | A5 | BCM 26 | Pin 37 | Output (Pi -> EEPROM) |
| Address Bus | A6 | BCM 20 | Pin 38 | Output (Pi -> EEPROM) |
| Address Bus | A7 | BCM 21 | Pin 40 | Output (Pi -> EEPROM) |
| Address Bus | A8 | BCM 19 | Pin 35 | Output (Pi -> EEPROM) |
| Data I/O Bus | I/O0 | BCM 17 | Pin 11 | Input (EEPROM -> Pi) |
| Data I/O Bus | I/O1 | BCM 18 | Pin 12 | Input (EEPROM -> Pi) |
| Data I/O Bus | I/O2 | BCM 27 | Pin 13 | Input (EEPROM -> Pi) |
| Data I/O Bus | I/O3 | BCM 22 | Pin 15 | Input (EEPROM -> Pi) |
| Data I/O Bus | I/O4 | BCM 23 | Pin 16 | Input (EEPROM -> Pi) |
| Data I/O Bus | I/O5 | BCM 24 | Pin 18 | Input (EEPROM -> Pi) |
| Data I/O Bus | I/O6 | BCM 25 | Pin 22 | Input (EEPROM -> Pi) |
| Data I/O Bus | I/O7 | BCM 12 | Pin 32 | Input (EEPROM -> Pi) |

## Execution & Verification Workflow

Executing the verification harness on the host ARMv6 architecture (Raspberry Pi Zero W) requires the Mono CLR runtime.

### Prerequisites & Dependencies

Ensure the system has Mono installed and standard GPIO permissions enabled for the execution user:
`sudo apt-get update`
`sudo apt-get install mono-complete`

### Running the Test Suite

1. Build or transfer the compiled verification binary (`OfflineValidator.exe`) and native hardware wrappers to the Pi.
2. Execute the verification suite with elevated permissions to allow raw memory access to GPIO registers:
`sudo mono OfflineValidator.exe`

