temp_cdmx=[15.4, 18.1, 19.1, 20.2, 21.4, 20.1, 18.9, 19.8, 19.4, 19.1, 17.9, 15.7]

def sensacion_temp(temp):
    if temp > 30:
        return "Calor pesado"
    elif 24 <= temp <= 30:
        return "Calor moderado"
    elif 18 <= temp <= 24:
        return "Calor placentero"
    elif 12 <= temp <= 18:
        return "Placentero"
    elif 6 <= temp <= 12:
        return "Fresco"
    elif 0 <= temp <= 6:
        return "Muy fresco"
    elif -6 <= temp <= 0:
        return "Frío ligero"
    elif -12 <= temp <= -6:
        return "Frío"
    elif -18 <= temp <= -12:
        return "Muy frío"
    elif -24 <= temp <= -18:
        return "Frío intenso"
    else:
        return "Peligro de congelación"
        
sensacion_cdmx = [] 

for temp in temp_cdmx:
    sensacion = sensacion_temp(temp)
    sensacion_cdmx.append(sensacion)
    
#for sensacion in sensacion_cdmx:
    #print("Sensación térmica:", sensacion)
    
for temp, sensacion in zip(temp_cdmx, sensacion_cdmx):
    print(f"Temperatura:", temp, "Sensación térmica: ", sensacion)
    
#sensacion_mas_frecuente = ""
#max_contador = 0
#contador = 0
#i = 0

#while i < len(sensacion_cdmx):
   # contador = sensacion_cdmx.count(sensacion_cdmx[i])
    #if contador > max_contador:
       # max_contador = contador
       # sensacion_mas_frecuente = sensacion_cdmx[i]
   # i += 1

#print("La sensación térmica que más aparece es:", sensacion_mas_frecuente)