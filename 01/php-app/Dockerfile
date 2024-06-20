FROM php:7.4-fpm

RUN apt-get update && apt-get install -y libpq-dev libzip-dev zip \
    && docker-php-ext-install pdo_mysql zip \
    && pecl install redis \
    && docker-php-ext-enable redis
