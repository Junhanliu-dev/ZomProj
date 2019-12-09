import threading

from bitarray import bitarray
import mmh3
import random


class BloomFilter(object):
    _instance = None
    _lock = threading.Lock()

    def __new__(cls):
        if BloomFilter._instance is None:
            with BloomFilter._lock:
                if BloomFilter._instance is None:
                    BloomFilter._instance = super(BloomFilter, cls).__new__(cls)

        return BloomFilter._instance

    def __init__(self):
        self.hash_count = 10
        self.size = 1_000_000
        self.seeds = [random.randint(1, 101) for _ in range(self.hash_count)]
        self.bit_array = bitarray(self.size)
        self.bit_array.setall(0)

    def add(self, res_id):

        for seed in self.seeds:
            result = mmh3.hash(str(res_id), seed=seed) % self.size
            self.bit_array[result] = 1

    def lookup(self, res_id) -> bool:

        for seed in self.seeds:
            result = mmh3.hash(str(res_id), seed=seed) % self.size
            if self.bit_array[result] == 0:
                return False

        return True

    def get_array_size(self):
        return len(self.bit_array)

    def get_hash_count(self):
        return self.hash_count

    def get_seeds(self):
        return self.seeds

    def get_bit_array(self):
        return self.bit_array

    def read_bloom_from_file(self):
        pass

    def write_bloom_from_file(self):
        pass