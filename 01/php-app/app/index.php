<?php

// Test connection
try {
    $redis = new Redis();
    $redis->connect('redis', 6379);
    $redis->set('test_key', 'Hello world');
    $value = $redis->get('test_key');
    echo "Test connection to Redis by fetching the value of 'test_key' from PHP: '$value'";
} catch (Exception $e) {
    echo "No se pudo conectar a Redis: " . $e->getMessage();
}
