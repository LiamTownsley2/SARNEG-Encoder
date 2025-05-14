# Search and Rescue Numerical Encryption Grid (SARNEG)

SARNEG is a lightweight encryption method designed for securely transmitting **numerical information**, such as **map grid coordinates**, over **non-secure communication channels**. It is ideal for **Search and Rescue (SAR)** scenarios, where isolated individuals must communicate their location discreetly. (Chesbro, 2018)

---

## 🔐 How It Works

SARNEG substitutes each digit from `0–9` with a corresponding letter from a **10-letter keyword** (with no repeating letters).

### Step-by-Step:

1. **Choose a 10-letter keyword** (no repeated letters)  
   _Examples: `AFTERSHOCK`, `BLACKHORSE`, `CORNFLAKES`, `COMBATHELP`_

2. **Create the mapping**:

   ```
   Digits:   0 1 2 3 4 5 6 7 8 9
   Keyword:  A F T E R S H H O C K
   ```

3. **Encrypt the numeric portion** of your message using this mapping.
## 🔄 Example

### Input Grid Coordinate (MGRS):

```
45V UC 76232 63380
```

Using keyword `AFTERSHOCK`:

```
76232 63380 → OHTET HEECA
```

### Encrypted Message:
```
SARNEG: OHTET HEECA
```

_Note: Prefix (`45V UC`) is typically not transmitted unless necessary. The application is still however capable of handling these inputs._

![Demonstration Screenshot of the Console App](https://i.imgur.com/N2lUSso.png)

## ⚠️ Security Note

SARNEG is **not a strong encryption method**. It is suitable for:
- Obscuring location in hostile environments.
- Short-term secrecy of numerical data.
- Human memorization and quick encoding.

It is **not recommended** for encrypting sensitive or high-volume data.
## References
Chesbro, M., 2018. Operating in Hostile and Non-Permissive Environments: A Survival and Resource Guide For Those Who Go in Harm's Way. pp.172–173.
## Authors

- [@LiamTownsley2](https://www.github.com/LiamTownsley2)