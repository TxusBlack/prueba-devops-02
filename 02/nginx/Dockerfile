# Usa la imagen oficial de NGINX
FROM nginx:latest

# Copia el archivo de configuración personalizado de NGINX
COPY nginx.conf /etc/nginx/nginx.conf

# Exponer los puertos HTTP y HTTPS
EXPOSE 80
EXPOSE 443

# Iniciar NGINX en primer plano
CMD ["nginx", "-g", "daemon off;"]
