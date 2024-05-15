﻿using System;
using System.Text;
using System.Collections.Generic;

public static class CustomDescriptionDictionaries
{
    public static string GetEnsembleDescription(string code)
    {
        if (ensembleDescriptions.TryGetValue(code, out var description))
        {
            return description;
        }

        // Return empty string if the code is not found
        return "";
    }

    public static string GetGenreDescription(string code)
    {
        if (genreDescriptions.TryGetValue(code, out var description))
        {
            return description;
        }

        // Return empty string if the code is not found
        return "";
    }

    private static readonly Dictionary<string, string> ensembleDescriptions = new Dictionary<string, string>
    {
        {"KM", "KM laulu- ja puheäänet, miesääniä"},
        {"KM1", "KM1 laulu- ja puheäänet, miesääni"},
        {"KM2", "KM2 laulu- ja puheäänet, miesääniä (2)"},
        {"KM3", "KM3 laulu- ja puheäänet, miesääniä (3)"},
        {"KM4", "KM4 laulu- ja puheäänet, miesääniä (4)"},
        {"KM5", "KM5 laulu- ja puheäänet, miesääniä (5)"},
        {"KM6", "KM6 laulu- ja puheäänet, miesääniä (6)"},
        {"KM7", "KM7 laulu- ja puheäänet, miesääniä (7)"},
        {"KM8", "KM8 laulu- ja puheäänet, miesääniä (8)"},
        {"KM9", "KM9 laulu- ja puheäänet, miesääniä (9)"},

        {"KN", "KN laulu- ja puheäänet, naisääniä"},
        {"KN1", "KN1 laulu- ja puheäänet, naisääni"},
        {"KN2", "KN2 laulu- ja puheäänet, naisääniä (2)"},
        {"KN3", "KN3 laulu- ja puheäänet, naisääniä (3)"},
        {"KN4", "KN4 laulu- ja puheäänet, naisääniä (4)"},
        {"KN5", "KN5 laulu- ja puheäänet, naisääniä (5)"},
        {"KN6", "KN6 laulu- ja puheäänet, naisääniä (6)"},
        {"KN7", "KN7 laulu- ja puheäänet, naisääniä (7)"},
        {"KN8", "KN8 laulu- ja puheäänet, naisääniä (8)"},
        {"KN9", "KN9 laulu- ja puheäänet, naisääniä (9)"},

        {"KQ", "KQ laulu- ja puheäänet, nais- ja miesääniä"},
        {"KQ1", "KQ laulu- ja puheäänet, nais- ja miesääniä"},
        {"KQ2", "KQ2 laulu- ja puheäänet, nais- ja miesääni"},
        {"KQ3", "KQ3 laulu- ja puheäänet, nais- ja miesääniä (3)"},
        {"KQ4", "KQ4 laulu- ja puheäänet, nais- ja miesääniä (4)"},
        {"KQ5", "KQ5 laulu- ja puheäänet, nais- ja miesääniä (5)"},
        {"KQ6", "KQ6 laulu- ja puheäänet, nais- ja miesääniä (6)"},
        {"KQ7", "KQ7 laulu- ja puheäänet, nais- ja miesääniä (7)"},
        {"KQ8", "KQ8 laulu- ja puheäänet, nais- ja miesääniä (8)"},
        {"KQ9", "KQ9 laulu- ja puheäänet, nais- ja miesääniä (9)"},

        {"KV", "KV laulu- ja puheäänet, ihmisääniä (epämääräinen, esim. vihellys)"},
        {"KV1", "KV1 laulu- ja puheäänet, ihmisääni (epämääräinen, esim. vihellys)"},
        {"KV2", "KV2 laulu- ja puheäänet, ihmisääniä (2) (epämääräinen, esim. vihellys)"},
        {"KV3", "KV3 laulu- ja puheäänet, ihmisääniä (3) (epämääräinen, esim. vihellys)"},
        {"KV4", "KV4 laulu- ja puheäänet, ihmisääniä (4) (epämääräinen, esim. vihellys)"},
        {"KV5", "KV5 laulu- ja puheäänet, ihmisääniä (5) (epämääräinen, esim. vihellys)"},
        {"KV6", "KV6 laulu- ja puheäänet, ihmisääniä (6) (epämääräinen, esim. vihellys)"},
        {"KV7", "KV7 laulu- ja puheäänet, ihmisääniä (7) (epämääräinen, esim. vihellys)"},
        {"KV8", "KV8 laulu- ja puheäänet, ihmisääniä (8) (epämääräinen, esim. vihellys)"},
        {"KV9", "KV9 laulu- ja puheäänet, ihmisääniä (9) (epämääräinen, esim. vihellys)"},

        {"KS", "KS soitinyhtye, säestävä (laadusta riippumatta)"},
        {"KS1", "KS1 soitin, säestävä"},
        {"KS2", "KS2 soitinyhtye, säestävä (2 soitinta)"},
        {"KS3", "KS3 soitinyhtye, säestävä (3 soitinta)"},
        {"KS4", "KS4 soitinyhtye, säestävä (4 soitinta)"},
        {"KS5", "KS5 soitinyhtye, säestävä (5 soitinta)"},

        {"KZ", "KZ soitinyhtye tai soitinsolisteja"},
        {"KZ1", "KZ1 soitin, soitinsolisti (puhdas soolo tai solistinen osuus)"},
        {"KZ2", "KZ2 soitinyhtye tai soitinsolisteja (2), duo"},
        {"KZ3", "KZ3 soitinyhtye tai soitinsolisteja (3), trio"},
        {"KZ4", "KZ4 soitinyhtye tai soitinsolisteja (4), kvartetti"},
        {"KZ5", "KZ5 soitinyhtye tai soitinsolisteja (5), kvintetti"},
        {"KZ6", "KZ6 soitinyhtye tai soitinsolisteja (6), sekstetti"},
        {"KZ7", "KZ7 soitinyhtye tai soitinsolisteja (7), septetti"},
        {"KZ8", "KZ8 soitinyhtye tai soitinsolisteja (8), oktetti"},
        {"KZ9", "KZ9 soitinyhtye tai soitinsolisteja (9), nonetti"},

        {"KI", "KI instrumentaaliesitys"}
    };

    private static readonly Dictionary<string, string> genreDescriptions = new Dictionary<string, string>
    {
        {"KMUS", "KMUS kevyt musiikki"},
        {"L0", "L0 PUHE-ESITYKSET"},
        {"L1", "L1 TAIDEMUSIIKKI"},
        {"L2", "L2 USKONNOLLINEN MUSIIKKI"},
        {"L3", "L3 KANSANMUSIIKKI"},
        {"L4", "L4 POP/ROCK"},
        {"L5", "L5 JAZZ JA BLUES"},
        {"L6", "L6 MUU KEVYT MUSIIKKI"},
        {"L9", "L9 ERIKOISÄÄNITTEET"},

        {"L0A", "L0A näyttämöteokset"},
        {"L0B", "L0B proosa"},
        {"L0C", "L0C lyriikka"},
        {"L0D", "L0D selostukset, puheet, ym dokumentit"},
        {"L0E", "L0E opetus"},
        {"L0L", "L0L sadut, yms lapsille"},
        {"L0U", "L0U uskonnolliset puhe-esitykset"},
        {"L0X", "L0X muut puhe-esitykset"},
        {"L1A", "L1A näyttämömusiikki"},
        {"L1AS", "L1AS sovitettu näyttämömusiikki"},
        {"L1B", "L1B moniääninen vokaalimusiikki"},
        {"L1BS", "L1BS sovitettu moniääninen vokaalimusiikki"},
        {"L1C", "L1C yksiääninen vokaalimusiikki"},
        {"L1CS", "L1CS sovitettu yksiääninen vokaalimusiikki"},
        {"L1D", "L1D orkesterimusiikki"},
        {"L1DS", "L1DS sovitettu orkesterimusiikki"},
        {"L1E", "L1E kamarimusiikki"},
        {"L1ES", "L1ES sovitettu kamarimusiikki"},
        {"L1F", "L1F soitinsoolot"},
        {"L1FS", "L1FS sovitetut soitinsoolot"},
        {"L1L", "L1L lastentaidemusiikki"},
        {"L1LS", "L1LS sovitettu lastentaidemusiikki"},
        {"L1U", "L1U uusi musiikki"},
        {"L1V", "L1V vanha musiikki"},
        {"L1VS", "L1VS sovitettu vanha musiikki"},
        {"L1X", "L1X muu taidemusiikki"},
        {"L2A", "L2A taidemusiikki (kristillinen)"},
        {"L2AA", "L2AA taidemusiikki (kristillinen): laajamuotoinen"},
        {"L2B", "L2B muu uskonnollinen musiikki (virret, hengelliset laulut, ym)"},
        {"L2BB", "L2BB muu uskonnollinen musiikki (virret, hengelliset laulut, ym): laajamuot"},
        {"L2L", "L2L lastenmusiikki"},
        {"L2N", "L2N populaarimusiikki"},
        {"L2X", "L2X ei kristinuskoon liittyvä musiikki"},
        {"L3A", "L3A perinteinen (perinteiseltä kuulostava)"},
        {"L3L", "L3L lasten kansanmusiikki"},
        {"L3U", "L3U uusi kansanmusiikki, maailmanmusiikki"},
        {"L3X", "L3X muu kansanmusiikki (sovitettu, ym. ei autenttinen)"},
        {"L4A", "L4A pop- ja/tai rock-tyylinen musiikki"},
        {"L4AA", "L4AA pop- ja/tai rock-tyylinen laajamuotinen musiikki"},
        {"L4L", "L4L lasten pop- ja/tai rock-tyylinen musiikki"},
        {"L5A", "L5A jazz"},
        {"L5B", "L5B blues"},
        {"L5L", "L5L lasten jazz ja blues"},
        {"L6A", "L6A näyttämömusiikki (operetti, musikaali, kabaree, revyy, show, ym)"},
        {"L6B", "L6B folk ja country"},
        {"L6C", "L6C iskelmätyyppinen ohjelmisto"},
        {"L6D", "L6D laulelmat" },
        {"L6L", "L6L lastenmusiikki"},
        {"L6T", "L6T (vanha) tanssimusiikki" },
        {"L6V", "L6V viihdemusiikki"},
        {"L6X", "L6X muu kevyt ohjelmisto"},
        {"L9A", "L9A musiikin opetus"},
        {"L9B", "L9B tehostemusiikki"},
        {"L9C", "L9C äänitehosteet"},
        {"L9D", "L9D tekniset kokeilulevyt"},
        {"L9E", "L9E elokuvamusiikki"},
        {"L9F", "L9F huumori, parodia, matkiminen"},
        {"L9G", "L9G syrjähypyt (kaksoiskoodaus)"},
        {"L9T", "L9T tunnussävelmät"},
        {"L9X", "L9X muut erikoisäänitteet"},
        {"VMUS", "VMUS taidemusiikki"}
    };
}
